namespace Shared.DTOs.WarehouseRoom
{
    public class WarehouseRoomDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Domain.Enums.WarehouseStatus Status { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? BranchId { get; set; }
        public string? BranchName { get; set; }
    }
}