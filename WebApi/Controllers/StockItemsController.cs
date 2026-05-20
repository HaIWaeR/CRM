using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.StockItem;
using Application.Behavior.StockItem;
using Domain.Entities;
using Mapster;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockItemsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateStockItemAsync([FromBody] CreateStockItemCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<StockItemDto>> GetAllStockItemsAsync()
        {
            List<StockItemEntity> stockItems = await mediator.Send(new GetAllStockItemsQuery());
            return stockItems.Adapt<List<StockItemDto>>();
        }

        [HttpGet("{id}")]
        public async Task<StockItemDto?> GetStockItemByIdAsync(Guid id)
        {
            StockItemEntity? stockItem = await mediator.Send(new GetStockItemByIdQuery { Id = id });
            return stockItem.Adapt<StockItemDto>();
        }

        [HttpPut("{id}")]
        public async Task<StockItemDto?> UpdateStockItemAsync(Guid id, [FromBody] UpdateStockItemCommand command)
        {
            command.Id = id;
            StockItemEntity? update = await mediator.Send(command);
            return update.Adapt<StockItemDto>();
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteStockItemAsync(Guid id)
        {
            return await mediator.Send(new DeleteStockItemCommand { Id = id });
        }
    }
}