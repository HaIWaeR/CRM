namespace WebApi.DTO.Order
{
    public class OrderToCreateOrUpdateDto
    {
        public string OrderNumber { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public Domain.Enums.OrderStatus Status { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? BranchId { get; set; }
    }
}