namespace Shared.DTOs.Client
{
    public class ClientToCreateOrUpdateDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Telegram { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
    }
}