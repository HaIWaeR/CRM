using MediatR;
using Application.Interfaces.Repositories;

namespace Application.Behavior.StockItem
{
    public class DeleteStockItemCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteStockItemCommandHandler(IStockItemRepository repository) : IRequestHandler<DeleteStockItemCommand, bool>
    {
        public async Task<bool> Handle(DeleteStockItemCommand command, CancellationToken cancellationToken)
        {
            if (!await repository.ExistsAsync(command.Id))
                throw new KeyNotFoundException($"Запись с ID {command.Id} не найдена");

            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}