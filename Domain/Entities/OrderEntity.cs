using Domain.Enums;

namespace Domain.Entities
{
    public class OrderEntity
    {
        public Guid Id { get; }
        public Guid? ClientId { get; private set; }
        public ClientEntity? Client { get; private set; }
        public string OrderNumber { get; private set; }
        public string ServiceName { get; private set; }
        public decimal Price { get; private set; }
        public OrderStatus Status { get; private set; }
        public string? Description { get; private set; }
        public string? Address { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime? UpdatedAt { get; private set; }
    }
}
