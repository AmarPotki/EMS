using EMS.Domain.AggregatesModel.FaultAggregate;
using EMS.Domain.AggregatesModel.FaultTypeAggregate;
using EMS.Domain.AggregatesModel.ItemTypeAggregate;
using EMS.Domain.AggregatesModel.LocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EMS.Infrastructure.EntityConfigurations{
    public class FaultEntityTypeConfiguration : IEntityTypeConfiguration<Fault>
    {
        public void Configure(EntityTypeBuilder<Fault> builder)
        {
            builder.ToTable("faults", EmsContext.DEFAULT_SCHEMA);
            builder.HasKey(l => l.Id);
            builder.Property(a => a.Title).IsRequired();
            builder.Property(a => a.Description).IsRequired();
            builder.Property(l => l.Id).ForSqlServerUseSequenceHiLo("faultseq", EmsContext.DEFAULT_SCHEMA);
            builder.Property<int>("LocationId").IsRequired();
            builder.Property<int>("FaultTypeId").IsRequired();
            builder.Property<int>("ItemTypeId").IsRequired();
            builder.Property<string>("OwnerId").IsRequired();
            builder.Property<int>("FaultStatusId").IsRequired();
            builder.Property<int>("FixUnitId").IsRequired();

            builder.OwnsOne(o => o.Assign);

            builder.Ignore(l => l.DomainEvents);
            var navigation = builder.Metadata.FindNavigation(nameof(Fault.Flows));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            var navFaultResult = builder.Metadata.FindNavigation(nameof(Fault.FaultResults));
            navFaultResult.SetPropertyAccessMode(PropertyAccessMode.Field);

            var navParts = builder.Metadata.FindNavigation(nameof(Fault.ConsumeParts));
            navParts.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne<Location>()
                .WithMany()
                .IsRequired()
                .HasForeignKey("LocationId");
            builder.HasOne<ItemType>()
                .WithMany()
                .IsRequired()
                .HasForeignKey("ItemTypeId");
            builder.HasOne<FaultType>()
                .WithMany()
                .IsRequired()
                .HasForeignKey("FaultTypeId");
            builder.HasOne(o => o.Faultstatus)
                .WithMany()
                .HasForeignKey("FaultStatusId");
        }
    }
}