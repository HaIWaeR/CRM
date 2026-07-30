using Domain.Entities;

namespace Shared.DTOs.Material
{
    public class MaterialDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Article { get; set; } = string.Empty;
        public string CategoryCode { get; set; } = string.Empty;
        public decimal PriceUnit { get; set; }
        public decimal Weight { get; set; }
        public Domain.Enums.UnitMeasurement UnitMeasurement { get; set; }
        public string? Description { get; set; }
        public Dictionary<string, string>? Attributes { get; set; }
        public string? AdditionInformation { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}