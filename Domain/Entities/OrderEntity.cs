using Domain.Enums;

namespace Domain.Entities
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public Guid? ClientId { get; set; }
        public ClientEntity? Client { get; set; }
        public string OrderNumber { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public OrderStatus Status { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
