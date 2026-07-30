using Domain.Enums;

namespace Domain.Entities
{
    public class ClientEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        
        public DateTime? BirthDate { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set ; }
        public string? Telegram { get; set; }
        public long? TelegramId {  get; set; }

        public ClientStatus Status { get; set; } 
        public DateTime? LastActivityAt { get; set; }

        public string? Address { get; set; }
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<OrderEntity> Orders { get; set; } = [];
    }
}
