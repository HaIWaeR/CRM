using Application.Behavior.StockItem;
using Domain.Entities;
using Domain.Settings;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.DTOs.Pagination;
using Shared.DTOs.StockItem;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StockItemsController(IMediator mediator, IOptions<PaginationSettings> paginationOptions) : ControllerBase
    {
        private readonly PaginationSettings _paginationSettings = paginationOptions.Value;
        [Authorize(Roles = "Owner, Admin")]
        [HttpPost]
        public async Task<Guid> CreateStockItemAsync([FromBody] StockItemToCreateOrUpdateDto dto)
        {
            CreateStockItemCommand command = dto.Adapt<CreateStockItemCommand>();
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<PaginatedResult<StockItemDto>> GetAllStockItemsAsync(
            [FromQuery] int page = 1,
            [FromQuery] Guid? productId = null,
            [FromQuery] Guid? materialId = null,
            [FromQuery] Guid? warehouseId = null,
            [FromQuery] Guid? storageZoneId = null,
            [FromQuery] int? minQuantity = null,
            [FromQuery] int? maxQuantity = null)
        {
            int size = _paginationSettings.EntitySizes.StockItems;

            GetAllStockItemsQuery query = new GetAllStockItemsQuery
            {
                Page = page,
                Size = size,
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

        [Authorize(Roles = "Owner, Admin")]
        [HttpPut("{id}")]
        public async Task<StockItemDto> UpdateStockItemAsync(Guid id, [FromBody] StockItemToCreateOrUpdateDto dto)
        {
            UpdateStockItemCommand command = dto.Adapt<UpdateStockItemCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [Authorize(Roles = "Owner")]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteStockItemAsync(Guid id)
        {
            return await mediator.Send(new DeleteStockItemCommand { Id = id });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPatch("{id}/add")]
        public async Task<bool> AddQuantityAsync(Guid id, [FromBody] RemoveAndAddStockQuantityDto dto)
        {
            return await mediator.Send(new AddStockQuantityCommand
            {
                Id = id,
                Quantity = dto.Quantity
            });
        }

        [Authorize(Roles = "Owner, Admin")]
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