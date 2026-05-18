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
            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}