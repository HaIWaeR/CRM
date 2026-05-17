using Domain.Enums;

namespace Application.DTO
{
    public class StorageZoneDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public StorageZoneType ZoneType { get; set; }
        public int? MaxCapacity { get; set; }
        public Guid WarehouseId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}