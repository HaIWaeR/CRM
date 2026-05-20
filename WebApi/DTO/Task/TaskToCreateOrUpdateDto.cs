namespace WebApi.DTO.Task
{
    public class TaskToCreateOrUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Domain.Enums.InstallTaskStatus Status { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? UserId { get; set; }
    }
}