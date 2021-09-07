using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using BuildingBlocks.Infrastructure.Services.Idempotency.EntityConfigurations;
using BuildingBlocks.Infrastructure.Services.Mediator;
using EMS.Domain.AggregatesModel.UserProfileAggregate;
using Irvine.SeedWork.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace EMS.Infrastructure{
    public class UserProfileContext : IdentityDbContext<UserProfile>, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public DbSet<ClientRequest> ClientRequests;

        public UserProfileContext(DbContextOptions<UserProfileContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            System.Diagnostics.Debug.WriteLine("EMSContext::ctor ->" + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);
            var result = await SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}