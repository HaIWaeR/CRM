using Domain.Enums;

namespace Application.DTO
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public OrderStatus Status { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? BranchId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}