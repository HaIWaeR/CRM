using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.StockItem;

namespace Application.Behavior.StockItem
{
    public class GetStockItemByIdQuery : IRequest<StockItemDto>
    {
        public Guid Id { get; set; }
    }

    public class GetStockItemByIdQueryHandler(IStockItemRepository repository) : IRequestHandler<GetStockItemByIdQuery, StockItemDto>
    {
        public async Task<StockItemDto> Handle(GetStockItemByIdQuery query, CancellationToken cancellationToken)
        {
            StockItemEntity? stockItem = await repository.GetByIdAsync(query.Id)
                ?? throw new KeyNotFoundException($"Запись с ID {query.Id} не найдена");

            StockItemDto result = stockItem.Adapt<StockItemDto>();
            return result;
        }
    }
}