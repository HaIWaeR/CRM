using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DTO;
using Application.Behavior.WarehouseRoom;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseRoomsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateWarehouseRoomAsync([FromBody] CreateWarehouseRoomCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<WarehouseRoomDto>> GetAllWarehouseRoomsAsync()
        {
            return await mediator.Send(new GetAllWarehouseRoomsQuery());
        }

        [HttpGet("{id}")]
        public async Task<WarehouseRoomDto?> GetWarehouseRoomByIdAsync(Guid id)
        {
            return await mediator.Send(new GetWarehouseRoomByIdQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<WarehouseRoomDto?> UpdateWarehouseRoomAsync(Guid id, [FromBody] UpdateWarehouseRoomCommand command)
        {
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteWarehouseRoomAsync(Guid id)
        {
            return await mediator.Send(new DeleteWarehouseRoomCommand { Id = id });
        }
    }
}