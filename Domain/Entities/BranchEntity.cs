using Domain.Enums;

namespace Domain.Entities
{
    public class BranchEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public BranchStatus Status { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<UserEntity> Users { get; set; } = [];
        public List<OrderEntity> Orders { get; set; } = [];
        public List<WarehouseEntity> Warehouses { get; set; } = [];
    }
}
