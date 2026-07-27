using Domain.Enums;

namespace Shared.DTOs.StorageZone
{
    public class StorageZoneDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public StorageZoneType ZoneType { get; set; }
        public StorageZoneStatus Status { get; set; }
        public int? MaxCapacity { get; set; }
        public string? Description { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
    }
}