using Application.Behavior.WarehouseRoom;
using Domain.Enums;
using Domain.Settings;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Pagination;
using Shared.DTOs.WarehouseRoom;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseRoomsController(IMediator mediator, IOptions<PaginationSettings> paginationOptions) : ControllerBase
    {
        private readonly PaginationSettings _paginationSettings = paginationOptions.Value;
        [Authorize(Roles = "Owner, Admin")]
        [HttpPost]
        public async Task<Guid> CreateWarehouseRoomAsync([FromBody] WarehouseRoomToCreateOrUpdateDto dto)
        {
            CreateWarehouseRoomCommand command = dto.Adapt<CreateWarehouseRoomCommand>();
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<PaginatedResult<WarehouseRoomDto>> GetAllWarehouseRoomsAsync(
            [FromQuery] int page = 1,
            [FromQuery] string? searchTerm = null,
            [FromQuery] Guid? branchId = null,
            [FromQuery] WarehouseStatus? status = null)
        {
            int size = _paginationSettings.EntitySizes.WarehouseRooms;

            GetAllWarehouseRoomsQuery query = new GetAllWarehouseRoomsQuery
            {
                Page = page,
                Size = size,
                SearchTerm = searchTerm,
                BranchId = branchId,
                Status = status
            };

            return await mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<WarehouseRoomDto?> GetWarehouseRoomByIdAsync(Guid id)
        {
            return await mediator.Send(new GetWarehouseRoomByIdQuery { Id = id });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPut("{id}")]
        public async Task<WarehouseRoomDto?> UpdateWarehouseRoomAsync(Guid id, [FromBody] WarehouseRoomToCreateOrUpdateDto dto)
        {
            UpdateWarehouseRoomCommand command = dto.Adapt<UpdateWarehouseRoomCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [Authorize(Roles = "Owner")]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteWarehouseRoomAsync(Guid id)
        {
            return await mediator.Send(new DeleteWarehouseRoomCommand { Id = id });
        }
        [Authorize(Roles = "Owner, Admin")]
        [HttpPatch("{id}/status")]
        public async Task<bool> ChangeWarehouseRoomStatusAsync(Guid id, [FromBody] WarehouseStatus status)
        {
            return await mediator.Send(new ChangeWarehouseStatusCommand
            {
                Id = id,
                Status = status
            });
        }
    }
}