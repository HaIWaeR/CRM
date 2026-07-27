using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.StockItem;

namespace Application.Behavior.StockItem
{
    public class GetAllStockItemsQuery : IRequest<List<StockItemDto>>
    {
        public Guid? ProductId { get; set; }
        public Guid? MaterialId { get; set; }
        public Guid? WarehouseId { get; set; }
        public Guid? StorageZoneId { get; set; }
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }
    }

    public class GetAllStockItemsQueryHandler(IStockItemRepository repository) : IRequestHandler<GetAllStockItemsQuery, List<StockItemDto>>
    {
        public async Task<List<StockItemDto>> Handle(GetAllStockItemsQuery query, CancellationToken cancellationToken)
        {
            List<StockItemEntity> stockItems = await repository.GetFilteredAsync(
                query.ProductId,
                query.MaterialId,
                query.WarehouseId,
                query.StorageZoneId,
                query.MinQuantity,
                query.MaxQuantity);

            List<StockItemDto> result = stockItems.Adapt<List<StockItemDto>>();
            return result;
        }
    }
}