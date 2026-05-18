using Application.DTO;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Orders
{
    public class UpdateOrderCommand : IRequest<OrderDto?>
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

    public class UpdateOrderCommandHandler(IOrderRepository repository) : IRequestHandler<UpdateOrderCommand, OrderDto?>
    {
        public async Task<OrderDto?> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
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

            return new OrderDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                ServiceName = order.ServiceName,
                Price = order.Price,
                Status = order.Status,
                Description = order.Description,
                Address = order.Address,
                ClientId = order.ClientId,
                BranchId = order.BranchId
            };
        }
    }
}