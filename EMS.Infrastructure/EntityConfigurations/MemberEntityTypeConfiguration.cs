using EMS.Domain.AggregatesModel.FixUnitAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EMS.Infrastructure.EntityConfigurations{
    public class MemberEntityTypeConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("members", EmsContext.DEFAULT_SCHEMA);
            builder.HasKey(l => l.Id);
            builder.Property(a => a.IdentityGuid).IsRequired();
            builder.Property(l => l.Id).ForSqlServerUseSequenceHiLo("memeberseq", EmsContext.DEFAULT_SCHEMA);
            builder.Ignore(l => l.DomainEvents);
            builder.Property<int>("FixUnitId")
                .IsRequired();

        }
    }
}