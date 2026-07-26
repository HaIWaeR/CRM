using Domain.Enums;

namespace Domain.Entities
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public OrderStatus Status { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Guid? ClientId { get; set; }
        public ClientEntity? Client { get; set; }

        public Guid? BranchId { get; set; }
        public BranchEntity? Branch { get; set; }
    }
}
