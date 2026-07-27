namespace Domain.Entities.Supporting
{
    public class OrderItemEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public OrderEntity? Order { get; set; }
        public Guid ProductId { get; set; }
        public ProductEntity? Product { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}