using Domain.Enums;

namespace Shared.DTOs.StorageZone
{
    public class StorageZoneToCreateOrUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public int? MaxCapacity { get; set; }
        public string? Description { get; set; }
        public bool IsDefault { get; set; } = false;
        public Guid WarehouseId { get; set; }
    }
}