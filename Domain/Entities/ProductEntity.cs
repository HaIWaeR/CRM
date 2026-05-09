using Domain.Enums;

namespace Domain.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public ProductCategory Category { get; private set; }
        public string? Article { get; private set; }
        public string? Description { get; private set; }
        public Dictionary<string, string>? Attributes { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsService { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime? UpdatedAt { get; private set; }
    }
}
