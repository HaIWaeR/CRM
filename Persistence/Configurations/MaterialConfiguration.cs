using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class MaterialConfiguration : IEntityTypeConfiguration<MaterialEntity>
    {
        public void Configure(EntityTypeBuilder<MaterialEntity> builder)
        {
            builder.ToTable("Materials");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Article).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CategoryCode).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.PriceUnit).IsRequired().HasPrecision(18, 2);
            builder.Property(x => x.Weight).HasPrecision(18, 3);
            builder.Property(x => x.UnitMeasurement).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.AdditionInformation).HasMaxLength(500);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired(false);
        }
    }
}