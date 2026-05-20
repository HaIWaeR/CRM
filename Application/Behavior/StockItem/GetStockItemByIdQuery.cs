using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.StockItem
{
    public class GetStockItemByIdQuery : IRequest<StockItemEntity?>
    {
        public Guid Id { get; set; }
    }

    public class GetStockItemByIdQueryHandler(IStockItemRepository repository) : IRequestHandler<GetStockItemByIdQuery, StockItemEntity?>
    {
        public async Task<StockItemEntity?> Handle(GetStockItemByIdQuery query, CancellationToken cancellationToken)
        {
            StockItemEntity? item = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Остаток с ID {query.Id} не найден");

            return item;
        }
    }
}