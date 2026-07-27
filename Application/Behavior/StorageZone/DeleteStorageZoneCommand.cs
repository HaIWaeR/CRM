using MediatR;
using Application.Interfaces.Repositories;

namespace Application.Behavior.StorageZone
{
    public class DeleteStorageZoneCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteStorageZoneCommandHandler(IStorageZoneRepository repository) : IRequestHandler<DeleteStorageZoneCommand, bool>
    {
        public async Task<bool> Handle(DeleteStorageZoneCommand command, CancellationToken cancellationToken)
        {
            if (!await repository.ExistsAsync(command.Id))
                throw new KeyNotFoundException($"Зона хранения с ID {command.Id} не найдена");

            if (await repository.HasStockItemsAsync(command.Id))
                throw new InvalidOperationException("Нельзя удалить зону, так как на ней есть товары на складе!");

            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}