using EMS.Domain.AggregatesModel.ItemTypeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EMS.Infrastructure.EntityConfigurations{
    public class ItemTypeEntityTypeConfiguration:IEntityTypeConfiguration<ItemType>{
        public void Configure(EntityTypeBuilder<ItemType> builder){
            builder.ToTable("itemTypes", EmsContext.DEFAULT_SCHEMA);
            builder.HasKey(l => l.Id);
            builder.Property(a => a.Name).IsRequired();
            builder.Property(l => l.Id).ForSqlServerUseSequenceHiLo("itemTypeseq", EmsContext.DEFAULT_SCHEMA);
            builder.Ignore(l => l.DomainEvents);
            builder.Property<bool>("IsArchive").IsRequired();


            builder.Property<int?>("ParentId").IsRequired(false);
            builder.HasOne(a => a.Parent)
                .WithMany()
                .HasForeignKey("ParentId");
        }
    }
}