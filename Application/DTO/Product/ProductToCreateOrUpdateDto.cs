using Domain.Enums;

namespace Application.DTO
{
    public class ProductToCreateOrUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public string? Article { get; set; }
        public string? Description { get; set; }
        public Dictionary<string, string>? Attributes { get; set; }
        public bool IsActive { get; set; }
        public bool IsService { get; set; }
    }
}