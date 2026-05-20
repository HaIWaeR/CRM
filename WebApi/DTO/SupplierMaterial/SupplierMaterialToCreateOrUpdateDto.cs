namespace WebApi.DTO.SupplierMaterial
{
    public class SupplierMaterialToCreateOrUpdateDto
    {
        public Guid SupplierId { get; set; }
        public Guid MaterialId { get; set; }
        public decimal? Price { get; set; }
        public int? DeliveryDays { get; set; }
        public string? Note { get; set; }
    }
}