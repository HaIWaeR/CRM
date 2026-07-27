using MediatR;
using Application.Interfaces.Repositories;

namespace Application.Behavior.Product
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProductCommandHandler(IProductRepository repository) : IRequestHandler<DeleteProductCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            if (!await repository.ExistsAsync(command.Id))
                throw new KeyNotFoundException($"Товар с ID {command.Id} не найден");

            if (await repository.HasStockItemsAsync(command.Id))
                throw new InvalidOperationException("Нельзя удалить товар, так как он есть на складе!");

            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}