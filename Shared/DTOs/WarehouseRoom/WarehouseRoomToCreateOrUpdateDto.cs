namespace Shared.DTOs.WarehouseRoom
{
    public class WarehouseRoomToCreateOrUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? ContactPerson { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public string? Description { get; set; }
        public Guid? BranchId { get; set; }
    }
}