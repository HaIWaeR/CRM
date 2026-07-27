using Domain.Enums;

namespace WebApi.DTO.Product
{
    public class ProductToCreateOrUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public string? Article { get; set; }
        public string? Description { get; set; }
        public Dictionary<string, string>? Attributes { get; set; }
        public bool IsService { get; set; }
    }
}