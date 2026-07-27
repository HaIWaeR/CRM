namespace Shared.DTOs.Material
{
    public class MaterialToCreateOrUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Article { get; set; } = string.Empty;
        public string CategoryCode { get; set; } = "GEN";
        public decimal PriceUnit { get; set; }
        public decimal Weight { get; set; }
        public Domain.Enums.UnitMeasurement UnitMeasurement { get; set; }
        public string? Description { get; set; }
        public string? AdditionInformation { get; set; }
    }
}