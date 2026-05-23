using Domain.Enums;
namespace Domain.Entities
{
    public class MaterialEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Article { get; set; } = string.Empty;
        public string CategoryCode { get; set; } = "GEN";
        public int Quantity { get; set; }
        public decimal PriceUnit { get; set; }
        public UnitMeasurement UnitMeasurement {get; set; }
        public string? Description { get; set; }
        public string? CellZone { get; set; }
        public string? AdditionInforamtion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<SupplierMaterialEntity> SupplierMaterials { get; set; } = [];
        public List<StockItemEntity> StockItems { get; set; } = [];
    }
}
