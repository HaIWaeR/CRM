using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Order
{
    public class ChangeOrderStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
    }
    public class ChangeOrderStatusCommandHandler(IOrderRepository repository) : IRequestHandler<ChangeOrderStatusCommand, bool>
    {
        public async Task<bool> Handle(ChangeOrderStatusCommand command, CancellationToken cancellationToken)
        {
            OrderEntity? order = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Заказ с ID {command.Id} не найден");

            order.Status = command.Status;
            order.UpdatedAt = DateTime.UtcNow;

            if (command.Status == OrderStatus.Completed)
                order.CompletedAt = DateTime.UtcNow;
            else if (command.Status == OrderStatus.Cancelled)
                order.CancelledAt = DateTime.UtcNow;

            await repository.UpdateAsync(order);
            return true;
        }
    }
}
