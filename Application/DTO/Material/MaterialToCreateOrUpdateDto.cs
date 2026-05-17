using Domain.Enums;

namespace Application.DTO
{
    public class MaterialToCreateOrUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Article { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PriceUnit { get; set; }
        public UnitMeasurement UnitMeasurement { get; set; }
        public string? Description { get; set; }
        public string? CellZone { get; set; }
        public string? AdditionInforamtion { get; set; }
    }
}