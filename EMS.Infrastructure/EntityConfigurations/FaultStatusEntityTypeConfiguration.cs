using EMS.Domain.AggregatesModel.FaultAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EMS.Infrastructure.EntityConfigurations{
    public  class FaultStatusEntityTypeConfiguration
        : IEntityTypeConfiguration<FaultStatus>
    {
        public void Configure(EntityTypeBuilder<FaultStatus> builder)
        {
            builder.ToTable("faultStatus", EmsContext.DEFAULT_SCHEMA);
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();
            builder.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}