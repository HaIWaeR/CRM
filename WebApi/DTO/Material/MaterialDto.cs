namespace WebApi.DTO.Material
{
    public class MaterialDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Article { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PriceUnit { get; set; }
        public Domain.Enums.UnitMeasurement UnitMeasurement { get; set; }
        public string? Description { get; set; }
        public string? CellZone { get; set; }
        public string? AdditionInforamtion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}