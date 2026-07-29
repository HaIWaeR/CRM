using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Behavior.WarehouseRoom
{
    public class DeleteWarehouseRoomCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteWarehouseRoomCommandHandler(IWarehouseRoomRepository repository) : IRequestHandler<DeleteWarehouseRoomCommand, bool>
    {
        public async Task<bool> Handle(DeleteWarehouseRoomCommand command, CancellationToken cancellationToken)
        {
            if (!await repository.ExistsAsync(command.Id))
                throw new KeyNotFoundException($"Склад с ID {command.Id} не найден");

            if (await repository.HasStorageZonesAsync(command.Id))
                throw new InvalidOperationException("Нельзя удалить склад, так как на нём есть зоны хранения!");

            if (await repository.HasStockItemsAsync(command.Id))
                throw new InvalidOperationException("Нельзя удалить склад, так как на нём есть товары!");

            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}