using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

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
            StockItemEntity stockItem = command.Adapt<StockItemEntity>();
            stockItem.Id = Guid.NewGuid();
            stockItem.LastUpdate = DateTime.UtcNow;

            await repository.AddAsync(stockItem);
            return stockItem.Id;
        }
    }
}