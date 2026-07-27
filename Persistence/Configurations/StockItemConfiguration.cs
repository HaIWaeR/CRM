using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class StockItemConfiguration : IEntityTypeConfiguration<StockItemEntity>
    {
        public void Configure(EntityTypeBuilder<StockItemEntity> builder)
        {
            builder.ToTable("StockItems");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.LastUpdate).IsRequired();
            builder.HasOne(x => x.Warehouse).WithMany().HasForeignKey(x => x.WarehouseId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.StorageZone).WithMany().HasForeignKey(x => x.StorageZoneId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Material).WithMany().HasForeignKey(x => x.MaterialId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}