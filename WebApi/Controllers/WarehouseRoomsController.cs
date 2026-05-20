using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.WarehouseRoom;
using Application.Behavior.WarehouseRoom;
using Domain.Entities;
using Mapster;

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
            List<WarehouseRoomEntity> warehouses = await mediator.Send(new GetAllWarehouseRoomsQuery());
            return warehouses.Adapt<List<WarehouseRoomDto>>();
        }

        [HttpGet("{id}")]
        public async Task<WarehouseRoomDto?> GetWarehouseRoomByIdAsync(Guid id)
        {
            WarehouseRoomEntity? warehouse = await mediator.Send(new GetWarehouseRoomByIdQuery { Id = id });
            return warehouse.Adapt<WarehouseRoomDto>();
        }

        [HttpPut("{id}")]
        public async Task<WarehouseRoomDto?> UpdateWarehouseRoomAsync(Guid id, [FromBody] UpdateWarehouseRoomCommand command)
        {
            command.Id = id;
            WarehouseRoomEntity? update = await mediator.Send(command);
            return update.Adapt<WarehouseRoomDto>();
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteWarehouseRoomAsync(Guid id)
        {
            return await mediator.Send(new DeleteWarehouseRoomCommand { Id = id });
        }
    }
}