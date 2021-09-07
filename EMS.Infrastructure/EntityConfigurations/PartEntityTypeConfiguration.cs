using EMS.Domain.AggregatesModel.PartAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EMS.Infrastructure.EntityConfigurations{
    public class PartEntityTypeConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.ToTable("parts", EmsContext.DEFAULT_SCHEMA);
            builder.HasKey(l => l.Id);
            builder.Property(a => a.Name).IsRequired();
            builder.Property(l => l.Id).ForSqlServerUseSequenceHiLo("partseq", EmsContext.DEFAULT_SCHEMA);
            builder.Property<bool>("IsArchive").IsRequired();

            builder.Ignore(l => l.DomainEvents);

      
        }
    }
}