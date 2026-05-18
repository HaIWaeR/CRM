using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DTO;
using Application.Behavior.StockItem;

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
            return await mediator.Send(new GetAllStockItemsQuery());
        }

        [HttpGet("{id}")]
        public async Task<StockItemDto?> GetStockItemByIdAsync(Guid id)
        {
            return await mediator.Send(new GetStockItemByIdQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<StockItemDto?> UpdateStockItemAsync(Guid id, [FromBody] UpdateStockItemCommand command)
        {
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteStockItemAsync(Guid id)
        {
            return await mediator.Send(new DeleteStockItemCommand { Id = id });
        }
    }
}