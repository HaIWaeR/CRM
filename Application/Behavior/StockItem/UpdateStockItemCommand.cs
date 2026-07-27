using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.StockItem;

namespace Application.Behavior.StockItem
{
    public class UpdateStockItemCommand : IRequest<StockItemDto>
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid? StorageZoneId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? MaterialId { get; set; }
    }

    public class UpdateStockItemCommandHandler(IStockItemRepository repository) : IRequestHandler<UpdateStockItemCommand, StockItemDto>
    {
        public async Task<StockItemDto> Handle(UpdateStockItemCommand command, CancellationToken cancellationToken)
        {
            StockItemEntity? existing = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Запись с ID {command.Id} не найдена");

            command.Adapt(existing);
            existing.LastUpdate = DateTime.UtcNow;

            await repository.UpdateAsync(existing);

            StockItemDto result = existing.Adapt<StockItemDto>();
            return result;
        }
    }
}