using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("SupplierMaterials")]
    public class SupplierMaterialEntity
    {
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public Guid MaterialId { get; set; }
        public decimal? Price { get; set; }
        public int? DeliveryDays { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public SupplierEntity Supplier { get; set; } = null!;
        public MaterialEntity Material { get; set; } = null!;
    }
}
