using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class SupplierMaterialConfiguration : IEntityTypeConfiguration<SupplierMaterialEntity>
    {
        public void Configure(EntityTypeBuilder<SupplierMaterialEntity> builder)
        {
            builder.ToTable("SupplierMaterials");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PriceUnit).HasPrecision(18, 2);
            builder.Property(x => x.DeliveryDays);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);
        }
    }
}