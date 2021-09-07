using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Idempotency.EntityConfigurations;
using BuildingBlocks.Infrastructure.Services.Mediator;
using EMS.Domain.AggregatesModel.FaultAggregate;
using EMS.Domain.AggregatesModel.FaultTypeAggregate;
using EMS.Domain.AggregatesModel.FixUnitAggregate;
using EMS.Domain.AggregatesModel.ItemTypeAggregate;
using EMS.Domain.AggregatesModel.LocationAggregate;
using EMS.Domain.AggregatesModel.PartAggregate;
using EMS.Infrastructure.EntityConfigurations;
using Irvine.SeedWork.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
namespace EMS.Infrastructure{
    public class EmsContext : DbContext,IUnitOfWork{
        public const string DEFAULT_SCHEMA = "ems";
        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction => _currentTransaction;
        public DbSet<Location> Locations { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<FixUnit> FixUnits { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<FaultType> FaultTypes { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Fault> Faults { get; set; }
        public DbSet<FaultStatus> FaultStatuses { get; set; }
        public DbSet<Flow> Flows { get; set; }
        public DbSet<FaultResult> FaultResults { get; set; }
        public DbSet<ConsumePart> ConsumeParts { get; set; }

        public EmsContext(DbContextOptions<EmsContext> options):base(options){
            
        }
        public EmsContext(DbContextOptions<EmsContext> options,IMediator mediator) : base(options){
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Debug.WriteLine("EMSContext::ctor ->" + GetHashCode());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LocationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ItemTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FixUnitEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MemberEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FaultTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FlowEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ConsumePartEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FaultEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FaultStatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FaultResultEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PartEntityTypeConfiguration());


        }
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken)){
            await _mediator.DispatchDomainEventsAsync(this);
            var result = await SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task BeginTransactionAsync()
        {
            _currentTransaction = _currentTransaction ?? await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
    public class EmsContextDesignFactory : IDesignTimeDbContextFactory<EmsContext>
    {
        public EmsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EmsContext>()
                .UseSqlServer("Server=srvsql2014;Initial Catalog=EMS;Persist Security Info=True;User ID=sa;Password=SP_F@rm");

            return new EmsContext(optionsBuilder.Options, new NoMediator());
        }

        class NoMediator : IMediator
        {
            public Task Publish(object notification, CancellationToken cancellationToken = new CancellationToken()){
                throw new NotImplementedException();
            }
            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }

            public Task Send(IRequest request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.CompletedTask;
            }
        }
    }
}