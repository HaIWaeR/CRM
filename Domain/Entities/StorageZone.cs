using Domain.Enums;

namespace Domain.Entities
{
    public class StorageZone
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public string? Code { get; private set; }
        public StorageZoneType ZoneType { get; private set; }
        public Guid WarehouseId { get; private set; }
        public int? MaxCapacity { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime? UpdatedAt { get; private set; }
    }
}
