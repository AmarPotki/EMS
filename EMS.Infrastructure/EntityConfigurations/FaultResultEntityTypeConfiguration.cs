using System;
using EMS.Domain.AggregatesModel.FaultAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EMS.Infrastructure.EntityConfigurations{
    public class FaultResultEntityTypeConfiguration : IEntityTypeConfiguration<FaultResult>
    {
        public void Configure(EntityTypeBuilder<FaultResult> builder)
        {
            builder.ToTable("faultResults", EmsContext.DEFAULT_SCHEMA);
            builder.HasKey(l => l.Id);
            builder.Ignore(l => l.DomainEvents);
            builder.Property(l => l.Id).ForSqlServerUseSequenceHiLo("faultResultseq", EmsContext.DEFAULT_SCHEMA);
            builder.Property<DateTimeOffset>("Time").IsRequired();
        }
    }
}