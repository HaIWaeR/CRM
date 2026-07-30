using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.Order;

namespace Application.Behavior.Orders
{
    public class UpdateOrderCommand : IRequest<OrderDto>
    {
        public Guid Id { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Address { get; set; }
    }

    public class UpdateOrderCommandHandler(IOrderRepository repository) : IRequestHandler<UpdateOrderCommand, OrderDto>
    {
        public async Task<OrderDto> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            OrderEntity? existing = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Заказ с ID {command.Id} не найден");

            existing.ServiceName = command.ServiceName;
            existing.Description = command.Description;
            existing.Address = command.Address;
            existing.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(existing);

            OrderDto result = existing.Adapt<OrderDto>();
            return result;
        }
    }
}