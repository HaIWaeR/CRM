using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Behavior.Material
{
    public class DeleteMaterialCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteMaterialCommandHandler(IMaterialRepository repository) : IRequestHandler<DeleteMaterialCommand, bool>
    {
        public async Task<bool> Handle(DeleteMaterialCommand command, CancellationToken cancellationToken)
        {
            if (!await repository.ExistsAsync(command.Id))
                throw new KeyNotFoundException($"Материал с ID {command.Id} не найден");

            if (await repository.HasSuppliersAsync(command.Id))
                throw new InvalidOperationException("Нельзя удалить материал, так как он привязан к поставщикам!");

            if (await repository.HasStockItemsAsync(command.Id))
                throw new InvalidOperationException("Нельзя удалить материал, так как он есть на складе!");

            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}