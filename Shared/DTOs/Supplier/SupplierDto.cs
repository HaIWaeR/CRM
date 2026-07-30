using Domain.Enums;

namespace Shared.DTOs.Supplier
{
    public class SupplierDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Inn { get; set; }
        public string? Kpp { get; set; }
        public string? Address { get; set; }
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public Dictionary<string, string>? BankDetails { get; set; }
        public string? Description { get; set; }
        public int? Rating { get; set; }
        public SupplierType SupplierType { get; set; }
        public SupplierStatus SupplierStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}