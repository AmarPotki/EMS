using EMS.Domain.AggregatesModel.FaultTypeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EMS.Infrastructure.EntityConfigurations{
    public class FaultTypeEntityTypeConfiguration : IEntityTypeConfiguration<FaultType>
    {
        public void Configure(EntityTypeBuilder<FaultType> builder)
        {
            builder.ToTable("faultTypes", EmsContext.DEFAULT_SCHEMA);
            builder.HasKey(l => l.Id);
            builder.Property(a => a.Name).IsRequired();
            builder.Property(l => l.Id).ForSqlServerUseSequenceHiLo("faultTypeseq", EmsContext.DEFAULT_SCHEMA);
            builder.Property<bool>("IsArchive").IsRequired();
            builder.Ignore(l => l.DomainEvents);
        }
    }
}