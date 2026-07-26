using Application.Behavior.Order;
using Application.Behavior.Orders;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Order;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateOrderAsync([FromBody] OrderToCreateOrUpdateDto dto)
        {
            CreateOrderCommand command = dto.Adapt<CreateOrderCommand>();
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<OrderDto>> GetAllOrdersAsync(
            [FromQuery] string? searchTerm = null,
            [FromQuery] Domain.Enums.OrderStatus? status = null,
            [FromQuery] Guid? clientId = null,
            [FromQuery] Guid? branchId = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null)
        {
            GetAllOrdersQuery query = new GetAllOrdersQuery
            {
                SearchTerm = searchTerm,
                Status = status,
                ClientId = clientId,
                BranchId = branchId,
                FromDate = fromDate,
                ToDate = toDate
            };

            return await mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<OrderDto> GetOrderByIdAsync(Guid id)
        {
            return await mediator.Send(new GetOrderByIdQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<OrderDto> UpdateOrderAsync(Guid id, [FromBody] OrderToCreateOrUpdateDto dto)
        {
            UpdateOrderCommand command = dto.Adapt<UpdateOrderCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            return await mediator.Send(new DeleteOrderCommand { Id = id });
        }

        [HttpPatch("{id}/status")]
        public async Task<bool> ChangeOrderStatusAsync(Guid id, [FromBody] Domain.Enums.OrderStatus status)
        {
            return await mediator.Send(new ChangeOrderStatusCommand
            {
                Id = id,
                Status = status
            });
        }
    }
}