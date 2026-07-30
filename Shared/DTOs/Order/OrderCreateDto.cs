namespace Shared.DTOs.Order
{
    public class OrderCreateDto
    {
        public string ServiceName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Address { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? BranchId { get; set; }
        public List<OrderItemRequestDto> OrderItems { get; set; } = [];
    }
}
