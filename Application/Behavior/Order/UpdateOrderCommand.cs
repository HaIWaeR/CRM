using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.Order;

namespace Application.Behavior.Orders
{
    public class UpdateOrderCommand : IRequest<OrderDto>
    {
        public Guid Id { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? BranchId { get; set; }
    }

    public class UpdateOrderCommandHandler(IOrderRepository repository) : IRequestHandler<UpdateOrderCommand, OrderDto>
    {
        public async Task<OrderDto> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            OrderEntity? existing = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Заказ с ID {command.Id} не найден");

            command.Adapt(existing);
            existing.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(existing);

            OrderDto result = existing.Adapt<OrderDto>();
            return result;
        }
    }
}