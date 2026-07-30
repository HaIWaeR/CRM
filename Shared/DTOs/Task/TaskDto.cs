using Domain.Enums;

namespace Shared.DTOs.Task
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskPriority Priority { get; set; }
        public InstallTaskStatus Status { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserInfoForTaskDto? User { get; set; }
        public OrderInfoForTaskDto? Order { get; set; }
        public ClientInfoForTaskDto? Client { get; set; }
    }
}