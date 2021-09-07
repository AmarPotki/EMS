using EMS.Domain.AggregatesModel.FaultAggregate;
using EMS.Domain.AggregatesModel.PartAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EMS.Infrastructure.EntityConfigurations{
    public class ConsumePartEntityTypeConfiguration : IEntityTypeConfiguration<ConsumePart>
    {
        public void Configure(EntityTypeBuilder<ConsumePart> builder){
            builder.ToTable("consumeParts", EmsContext.DEFAULT_SCHEMA);
            builder.HasKey(l => l.Id);
            builder.Ignore(l => l.DomainEvents);
            builder.Property(l => l.Id).ForSqlServerUseSequenceHiLo("consumePartseq", EmsContext.DEFAULT_SCHEMA);
            builder.Property<int>("FaultId")
                .IsRequired();
            builder.HasOne<Part>()
                .WithMany()
                .IsRequired()
                .HasForeignKey("PartId");
        }
    }
}