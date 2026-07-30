namespace Shared.DTOs.Task
{
    public class OrderInfoForTaskDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
    }
}