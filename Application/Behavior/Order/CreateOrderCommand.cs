using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Orders
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public string OrderNumber { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public OrderStatus Status { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? BranchId { get; set; }
    }
    public class CreateOrderCommandHandler(IOrderRepository repository) : IRequestHandler<CreateOrderCommand, Guid>
    {
        public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            OrderEntity order = new OrderEntity
            {
                Id = Guid.NewGuid(),
                OrderNumber = command.OrderNumber,
                ServiceName = command.ServiceName,
                Price = command.Price,
                Status = command.Status,
                Description = command.Description,
                Address = command.Address,
                ClientId = command.ClientId,
                BranchId = command.BranchId,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(order);
            return order.Id;
        }
    }
}
