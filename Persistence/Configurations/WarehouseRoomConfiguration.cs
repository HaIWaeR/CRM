using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class WarehouseRoomConfiguration : IEntityTypeConfiguration<WarehouseRoomEntity>
    {
        public void Configure(EntityTypeBuilder<WarehouseRoomEntity> builder)
        {
            builder.ToTable("Warehouses");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(WarehouseStatus.Active);
            builder.Property(x => x.ContactPerson).HasMaxLength(100);
            builder.Property(x => x.ContactPhone).HasMaxLength(20);
            builder.Property(x => x.ContactEmail).HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);
        }
    }
}