using EMS.Domain.AggregatesModel.LocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EMS.Infrastructure.EntityConfigurations{
    public class LocationEntityTypeConfiguration : IEntityTypeConfiguration<Location>{
        public void Configure(EntityTypeBuilder<Location> builder){
            builder.ToTable("locations", EmsContext.DEFAULT_SCHEMA);
            builder.HasKey(l => l.Id);
            builder.Property(a => a.Name).IsRequired();
            builder.Property(l => l.Id).ForSqlServerUseSequenceHiLo("locationseq", EmsContext.DEFAULT_SCHEMA);
            builder.Ignore(l => l.DomainEvents);
            builder.Property<bool>("IsArchive").IsRequired();


            builder.Property<int?>("ParentId").IsRequired(false);
            builder.HasOne(a => a.Parent)
                .WithMany()
                .HasForeignKey("ParentId");
        }
    }
}