using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Warehouses")]
    public class WarehouseRoomEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPhone { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Guid? BranchId { get; set; }
        public BranchEntity? Branch { get; set; }

        public List<StorageZoneEntity> StorageZones { get; set; } = [];
    }
}
