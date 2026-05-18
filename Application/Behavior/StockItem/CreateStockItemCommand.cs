using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.StockItem
{
    public class CreateStockItemCommand : IRequest<Guid>
    {
        public int Quantity { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid? StorageZoneId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? MaterialId { get; set; }
    }

    public class CreateStockItemCommandHandler(IStockItemRepository repository) : IRequestHandler<CreateStockItemCommand, Guid>
    {
        public async Task<Guid> Handle(CreateStockItemCommand command, CancellationToken cancellationToken)
        {
            StockItemEntity stockItem = new StockItemEntity
            {
                Id = Guid.NewGuid(),
                Quantity = command.Quantity,
                WarehouseId = command.WarehouseId,
                StorageZoneId = command.StorageZoneId,
                ProductId = command.ProductId,
                MaterialId = command.MaterialId,
                LastUpdate = DateTime.UtcNow
            };

            await repository.AddAsync(stockItem);
            return stockItem.Id;
        }
    }
}