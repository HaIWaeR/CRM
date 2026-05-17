using Domain.Enums;

namespace Application.DTO
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public string? Article { get; set; }
        public string? Description { get; set; }
        public Dictionary<string, string>? Attributes { get; set; }
        public bool IsActive { get; set; }
        public bool IsService { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}