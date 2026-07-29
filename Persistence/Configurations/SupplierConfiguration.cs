using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<SupplierEntity>
    {
        public void Configure(EntityTypeBuilder<SupplierEntity> builder)
        {
            builder.ToTable("Suppliers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Inn).HasMaxLength(12);
            builder.Property(x => x.Kpp).HasMaxLength(9);
            builder.Property(x => x.Address).HasMaxLength(300);
            builder.Property(x => x.ContactPerson).HasMaxLength(100);
            builder.Property(x => x.Phone).HasMaxLength(20);
            builder.Property(x => x.Email).HasMaxLength(100);
            builder.Property(x => x.Website).HasMaxLength(200);
            builder.Property(x => x.BankDetails)
                .HasColumnType("jsonb")
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(v, (System.Text.Json.JsonSerializerOptions?)null)
                );
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.Rating);
            builder.Property(x => x.SupplierType).IsRequired().HasDefaultValue(SupplierType.Wholesale);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);
        }
    }
}