using Domain.Enums;

namespace Shared.DTOs.Task
{
    public class TaskToCreateOrUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime? Deadline { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ClientId { get; set; }
    }
}