namespace Application.DTO
{
    public class SupplierToCreateOrUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Inn { get; set; }
        public string? Kpp { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? BankDetails { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}