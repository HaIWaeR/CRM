using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.StockItem;
using Shared.DTOs.Pagination;

namespace Application.Behavior.StockItem
{
    public class GetAllStockItemsQuery : IRequest<PaginatedResult<StockItemDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public Guid? ProductId { get; set; }
        public Guid? MaterialId { get; set; }
        public Guid? WarehouseId { get; set; }
        public Guid? StorageZoneId { get; set; }
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }
    }

    public class GetAllStockItemsQueryHandler(IStockItemRepository repository) : IRequestHandler<GetAllStockItemsQuery, PaginatedResult<StockItemDto>>
    {
        public async Task<PaginatedResult<StockItemDto>> Handle(GetAllStockItemsQuery query, CancellationToken cancellationToken)
        {
            List<StockItemEntity> stockItems = await repository.GetFilteredAsync(
                query.ProductId,
                query.MaterialId,
                query.WarehouseId,
                query.StorageZoneId,
                query.MinQuantity,
                query.MaxQuantity,
                query.Page,
                query.Size);

            int totalCount = await repository.GetTotalCountAsync(
                query.ProductId,
                query.MaterialId,
                query.WarehouseId,
                query.StorageZoneId,
                query.MinQuantity,
                query.MaxQuantity);

            int totalPages = (int)Math.Ceiling(totalCount / (double)query.Size);

            if (query.Page > totalPages && totalPages > 0)
            {
                return new PaginatedResult<StockItemDto>
                {
                    Items = new List<StockItemDto>(),
                    TotalCount = totalCount,
                    Page = query.Page,
                    Size = query.Size,
                    TotalPages = totalPages
                };
            }

            List<StockItemDto> items = stockItems.Adapt<List<StockItemDto>>();

            return new PaginatedResult<StockItemDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = query.Page,
                Size = query.Size,
                TotalPages = totalPages
            };
        }
    }
}