namespace Shared.DTOs.Task
{
    public class ClientInfoForTaskDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set;  }
        public string? Telegram { get; set; }
    }
}