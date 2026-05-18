using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.StockItem
{
    public class GetAllStockItemsQuery : IRequest<List<StockItemDto>>;

    public class GetAllStockItemsQueryHandler(IStockItemRepository repository) : IRequestHandler<GetAllStockItemsQuery, List<StockItemDto>>
    {
        public async Task<List<StockItemDto>> Handle(GetAllStockItemsQuery query, CancellationToken cancellationToken)
        {
            List<StockItemEntity> items = await repository.GetAllAsync();
            return items.Select(item => new StockItemDto
            {
                Id = item.Id,
                Quantity = item.Quantity,
                LastUpdate = item.LastUpdate,
                WarehouseId = item.WarehouseId,
                StorageZoneId = item.StorageZoneId,
                ProductId = item.ProductId,
                MaterialId = item.MaterialId
            }).ToList();
        }
    }
}