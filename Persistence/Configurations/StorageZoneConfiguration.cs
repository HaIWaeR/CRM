using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class StorageZoneConfiguration : IEntityTypeConfiguration<StorageZoneEntity>
    {
        public void Configure(EntityTypeBuilder<StorageZoneEntity> builder)
        {
            builder.ToTable("StorageZones");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Code).HasMaxLength(50);
            builder.Property(x => x.ZoneType).IsRequired();
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(StorageZoneStatus.Empty);
            builder.Property(x => x.MaxCapacity);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.IsDefault).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);
        }
    }
}