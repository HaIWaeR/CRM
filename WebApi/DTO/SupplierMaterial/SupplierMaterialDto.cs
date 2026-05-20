namespace WebApi.DTO.SupplierMaterial
{
    public class SupplierMaterialDto
    {
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public Guid MaterialId { get; set; }
        public decimal? Price { get; set; }
        public int? DeliveryDays { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}