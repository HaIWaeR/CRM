using Domain.Enums;

namespace Domain.Entities
{
    public class StorageZoneEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public StorageZoneType ZoneType { get; set; }
        public StorageZoneStatus Status { get; set; }
        public int? MaxCapacity { get; set; }
        public string? Description { get; set; }
        public bool IsDefault { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Guid WarehouseId { get; set; }
        public WarehouseRoomEntity? Warehouse { get; set; }
    }
}