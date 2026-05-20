using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.StockItem
{
    public class UpdateStockItemCommand : IRequest<StockItemEntity?>
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid? StorageZoneId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? MaterialId { get; set; }
    }

    public class UpdateStockItemCommandHandler(IStockItemRepository repository) : IRequestHandler<UpdateStockItemCommand, StockItemEntity?>
    {
        public async Task<StockItemEntity?> Handle(UpdateStockItemCommand command, CancellationToken cancellationToken)
        {
            StockItemEntity? item = await repository.GetByIdAsync(command.Id) ?? throw new Exception($"Остаток с ID {command.Id} не найден");
            item.Quantity = command.Quantity;
            item.WarehouseId = command.WarehouseId;
            item.StorageZoneId = command.StorageZoneId;
            item.ProductId = command.ProductId;
            item.MaterialId = command.MaterialId;
            item.LastUpdate = DateTime.UtcNow;
            await repository.UpdateAsync(item);

            return item;
        }
    }
}