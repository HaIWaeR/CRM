using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.Order;
using Application.Behavior.Orders;
using Domain.Entities;
using Mapster;

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
            List<OrderEntity> orders = await mediator.Send(new GetAllOrdersQuery());
            return orders.Adapt<List<OrderDto>>();

        }

        [HttpGet("{id}")]
        public async Task<OrderDto?> GetOrderByIdAsync(Guid id)
        {
            OrderEntity? order = await mediator.Send(new GetOrderByIdQuery { Id = id });
            return order.Adapt<OrderDto>();
        }

        [HttpPut("{id}")]
        public async Task<OrderDto?> UpdateOrderAsync(Guid id, [FromBody] UpdateOrderCommand command)
        {
            command.Id = id;
            OrderEntity? update = await mediator.Send(command);
            return update.Adapt<OrderDto>();
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            return await mediator.Send(new DeleteOrderCommand { Id = id });
        }
    }
}