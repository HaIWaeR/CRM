using Application.Helpers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Behavior.Orders
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public string ServiceName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? BranchId { get; set; }
    }
    public class CreateOrderCommandHandler(IOrderRepository repository) : IRequestHandler<CreateOrderCommand, Guid>
    {
        public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            OrderEntity order = command.Adapt<OrderEntity>();
            order.Id = Guid.NewGuid();
            order.OrderNumber = OrderNumberGenerator.Generate(command.ServiceName);
            order.Status = OrderStatus.New;
            order.CreatedAt = DateTime.UtcNow;

            await repository.AddAsync(order);
            return order.Id;
        }
    }
}
