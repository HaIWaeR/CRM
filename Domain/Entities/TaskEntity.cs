using Domain.Enums;

namespace Domain.Entities
{
    public class TaskEntity
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

        public Guid? UserId { get; set; }
        public UserEntity? User { get; set; }

        public Guid? OrderId { get; set; }
        public OrderEntity? Order { get; set; }

        public Guid? ClientId { get; set; }
        public ClientEntity? Client { get; set; }
    }
}
