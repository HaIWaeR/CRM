using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<BranchEntity>
    {
        public void Configure(EntityTypeBuilder<BranchEntity> builder)
        {
            builder.ToTable("Branches");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Status).HasDefaultValue(BranchStatus.Active);
            builder.Property(x => x.ContactPhone).HasMaxLength(20);
            builder.Property(x => x.ContactEmail).HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(500);
        }
    }
}