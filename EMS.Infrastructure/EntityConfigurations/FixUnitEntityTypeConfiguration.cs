using System;
using EMS.Domain.AggregatesModel.FixUnitAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EMS.Infrastructure.EntityConfigurations{
    public class FixUnitEntityTypeConfiguration : IEntityTypeConfiguration<FixUnit>{
        public void Configure(EntityTypeBuilder<FixUnit> builder){
            builder.ToTable("fixUnits", EmsContext.DEFAULT_SCHEMA);
            builder.HasKey(l => l.Id);
            builder.Property(a => a.Title).IsRequired();
            builder.Property(a => a.Description).IsRequired();
            builder.Property(l => l.Id).ForSqlServerUseSequenceHiLo("fixUnitseq", EmsContext.DEFAULT_SCHEMA);
            builder.Ignore(l => l.DomainEvents);

            builder.Property<int>("LocationId").IsRequired();
            builder.Property<int>("ItemTypeId").IsRequired();
            builder.Property<string>("Owner").IsRequired();

            var navigation = builder.Metadata.FindNavigation(nameof(FixUnit.Members));

            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}