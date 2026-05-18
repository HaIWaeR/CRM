using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.StockItem
{
    public class GetStockItemByIdQuery : IRequest<StockItemDto?>
    {
        public Guid Id { get; set; }
    }

    public class GetStockItemByIdQueryHandler(IStockItemRepository repository) : IRequestHandler<GetStockItemByIdQuery, StockItemDto?>
    {
        public async Task<StockItemDto?> Handle(GetStockItemByIdQuery query, CancellationToken cancellationToken)
        {
            StockItemEntity? item = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Остаток с ID {query.Id} не найден");
            return new StockItemDto
            {
                Id = item.Id,
                Quantity = item.Quantity,
                LastUpdate = item.LastUpdate,
                WarehouseId = item.WarehouseId,
                StorageZoneId = item.StorageZoneId,
                ProductId = item.ProductId,
                MaterialId = item.MaterialId
            };
        }
    }
}