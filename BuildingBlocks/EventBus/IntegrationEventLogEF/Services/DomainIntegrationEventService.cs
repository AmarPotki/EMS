using System;
using System.Data.Common;
using System.Threading.Tasks;
using BuildingBlocks.IntegrationEventLogEF.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Irvine.BuildingBlocks.EventBus.Abstractions;
using Irvine.BuildingBlocks.EventBus.Events;

namespace BuildingBlocks.IntegrationEventLogEF.Services
{
    public class DomainIntegrationEventService<TDbContext> : IDomainIntegrationEventService
        where TDbContext : DbContext
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly TDbContext _dbContext;
        private readonly IIntegrationEventLogService _eventLogService;

        public DomainIntegrationEventService(IEventBus eventBus, TDbContext dbContext,
        Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_dbContext.Database.GetDbConnection());
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            await SaveEventAndDomainContextChangesAsync(evt);
            _eventBus.Publish(evt);
            await _eventLogService.MarkEventAsPublishedAsync(evt);
        }

        private async Task SaveEventAndDomainContextChangesAsync(IntegrationEvent evt)
        {
            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
            await ResilientTransaction.New(_dbContext)
                .ExecuteAsync(async () => {
                    // Achieving atomicity between original ordering database operation and the IntegrationEventLog thanks to a local transaction
                    await _dbContext.SaveChangesAsync();
                    await _eventLogService.SaveEventAsync(evt, _dbContext.Database.CurrentTransaction.GetDbTransaction());
                });
        }
    }
}