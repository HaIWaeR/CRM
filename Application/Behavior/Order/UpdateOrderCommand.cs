using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Orders
{
    public class UpdateOrderCommand : IRequest<OrderEntity?>
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
    }

    public class UpdateOrderCommandHandler(IOrderRepository repository) : IRequestHandler<UpdateOrderCommand, OrderEntity?>
    {
        public async Task<OrderEntity?> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            OrderEntity? order = await repository.GetByIdAsync(command.Id) ?? throw new Exception($"Заказ с ID {command.Id} не найден");

            order.OrderNumber = command.OrderNumber;
            order.ServiceName = command.ServiceName;
            order.Price = command.Price;
            order.Status = command.Status;
            order.Description = command.Description;
            order.Address = command.Address;
            order.ClientId = command.ClientId;
            order.BranchId = command.BranchId;
            order.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(order);

            return order;
        }
    }
}