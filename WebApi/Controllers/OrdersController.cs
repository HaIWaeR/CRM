using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DTO;
using Application.Behavior.Orders;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateOrderAsync([FromBody] CreateOrderCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            return await mediator.Send(new GetAllOrdersQuery());
        }

        [HttpGet("{id}")]
        public async Task<OrderDto?> GetOrderByIdAsync(Guid id)
        {
            return await mediator.Send(new GetOrderByIdQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<OrderDto?> UpdateOrderAsync(Guid id, [FromBody] UpdateOrderCommand command)
        {
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            return await mediator.Send(new DeleteOrderCommand { Id = id });
        }
    }
}