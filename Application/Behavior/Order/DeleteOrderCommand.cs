using MediatR;
using Application.Interfaces.Repositories;

namespace Application.Behavior.Orders
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteOrderCommandHandler(IOrderRepository repository) : IRequestHandler<DeleteOrderCommand, bool>
    {
        public async Task<bool> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            if (!await repository.ExistsAsync(command.Id))
                throw new KeyNotFoundException($"Заказ с ID {command.Id} не найден");

            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}