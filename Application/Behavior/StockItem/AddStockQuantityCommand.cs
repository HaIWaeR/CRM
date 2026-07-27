using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Behavior.StockItem
{
    public class AddStockQuantityCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
    public class AddStockQuantityCommandHandler(IStockItemRepository repository) : IRequestHandler<AddStockQuantityCommand, bool>
    {
        public async Task<bool> Handle(AddStockQuantityCommand command, CancellationToken cancellationToken)
        {
            if (command.Quantity <= 0)
                throw new InvalidOperationException("Количество должно быть больше 0");

            await repository.AddQuantityAsync(command.Id, command.Quantity);
            return true;
        }
    }
}
