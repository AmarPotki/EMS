using System;
using EMS.Domain.AggregatesModel.FaultAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EMS.Infrastructure.EntityConfigurations{
    public class FlowEntityTypeConfiguration : IEntityTypeConfiguration<Flow>{
        public void Configure(EntityTypeBuilder<Flow> builder){
            builder.ToTable("flows", EmsContext.DEFAULT_SCHEMA);
            builder.HasKey(l => l.Id);
            builder.Ignore(l => l.DomainEvents);
            builder.Property(l => l.Id).ForSqlServerUseSequenceHiLo("flowseq", EmsContext.DEFAULT_SCHEMA);
            builder.Property<DateTimeOffset>("Time").IsRequired();

            builder.OwnsOne(o => o.From);
            builder.OwnsOne(f => f.To);

        }
    }
}