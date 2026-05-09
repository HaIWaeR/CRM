namespace Domain.Entities
{
    public class ClientEntity
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public string? Phone { get; private set; }
        public string? Email { get; private set; }
        public string? Telegram { get; private set; }
        public string? Address { get; private set; }
        public string? Notes { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime? UpdatedAt { get; private set; }
    }
}
