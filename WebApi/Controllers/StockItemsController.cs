using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Behavior.StockItem;
using Domain.Entities;
using Shared.DTOs.StockItem;
using Mapster;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockItemsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateStockItemAsync([FromBody] StockItemToCreateOrUpdateDto dto)
        {
            CreateStockItemCommand command = dto.Adapt<CreateStockItemCommand>();
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<StockItemDto>> GetAllStockItemsAsync(
            [FromQuery] Guid? productId = null,
            [FromQuery] Guid? materialId = null,
            [FromQuery] Guid? warehouseId = null,
            [FromQuery] Guid? storageZoneId = null,
            [FromQuery] int? minQuantity = null,
            [FromQuery] int? maxQuantity = null)
        {
            GetAllStockItemsQuery query = new GetAllStockItemsQuery
            {
                ProductId = productId,
                MaterialId = materialId,
                WarehouseId = warehouseId,
                StorageZoneId = storageZoneId,
                MinQuantity = minQuantity,
                MaxQuantity = maxQuantity
            };

            return await mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<StockItemDto> GetStockItemByIdAsync(Guid id)
        {
            return await mediator.Send(new GetStockItemByIdQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<StockItemDto> UpdateStockItemAsync(Guid id, [FromBody] StockItemToCreateOrUpdateDto dto)
        {
            UpdateStockItemCommand command = dto.Adapt<UpdateStockItemCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteStockItemAsync(Guid id)
        {
            return await mediator.Send(new DeleteStockItemCommand { Id = id });
        }

        [HttpPatch("{id}/add")]
        public async Task<bool> AddQuantityAsync(Guid id, [FromBody] RemoveAndAddStockQuantityDto dto)
        {
            return await mediator.Send(new AddStockQuantityCommand
            {
                Id = id,
                Quantity = dto.Quantity
            });
        }

        [HttpPatch("{id}/remove")]
        public async Task<bool> RemoveQuantityAsync(Guid id, [FromBody] RemoveAndAddStockQuantityDto dto)
        {
            return await mediator.Send(new RemoveStockQuantityCommand
            {
                Id = id,
                Quantity = dto.Quantity
            });
        }
    }
}