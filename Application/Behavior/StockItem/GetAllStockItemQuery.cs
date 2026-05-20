using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.StockItem
{
    public class GetAllStockItemsQuery : IRequest<List<StockItemEntity>>;

    public class GetAllStockItemsQueryHandler(IStockItemRepository repository) : IRequestHandler<GetAllStockItemsQuery, List<StockItemEntity>>
    {
        public async Task<List<StockItemEntity>> Handle(GetAllStockItemsQuery query, CancellationToken cancellationToken)
        {
            List<StockItemEntity> items = await repository.GetAllAsync();

            return items;
        }
    }
}