namespace Shared.DTOs.SupplierMaterial
{
    public class SupplierMaterialDto
    {
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public Guid MaterialId { get; set; }
        public string? MaterialName { get; set; }
        public decimal? PriceUnit { get; set; }
        public int? DeliveryDays { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}