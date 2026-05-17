using Domain.Enums;

namespace Application.DTO
{
    public class StorageZoneToCreateOrUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public StorageZoneType ZoneType { get; set; }
        public int? MaxCapacity { get; set; }
        public Guid WarehouseId { get; set; }
    }
}