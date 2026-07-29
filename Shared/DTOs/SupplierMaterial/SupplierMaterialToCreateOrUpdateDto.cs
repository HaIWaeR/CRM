namespace Shared.DTOs.SupplierMaterial
{
    public class SupplierMaterialToCreateOrUpdateDto
    {
        public Guid SupplierId { get; set; }
        public Guid MaterialId { get; set; }
        public decimal? PriceUnit { get; set; }
        public int? DeliveryDays { get; set; }
        public string? Description { get; set; }
    }
}