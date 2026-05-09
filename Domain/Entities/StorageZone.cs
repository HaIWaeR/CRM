using Domain.Enums;

namespace Domain.Entities
{
    public class StorageZone
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
        public StorageZoneType ZoneType { get; set; }
        public Guid WarehouseId { get; set; }
        public int? MaxCapacity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
