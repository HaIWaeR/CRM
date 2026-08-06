using Application.Behavior.StorageZone;
using Domain.Enums;
using Domain.Settings;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.DTOs.Pagination;
using Shared.DTOs.StorageZone;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StorageZonesController(IMediator mediator, IOptions<PaginationSettings> paginationOptions) : ControllerBase
    {
        private readonly PaginationSettings _paginationSettings = paginationOptions.Value;
        [Authorize(Roles = "Owner, Admin")]
        [HttpPost]
        public async Task<Guid> CreateStorageZoneAsync([FromBody] StorageZoneToCreateOrUpdateDto dto)
        {
            CreateStorageZoneCommand command = dto.Adapt<CreateStorageZoneCommand>();
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<PaginatedResult<StorageZoneDto>> GetAllStorageZonesAsync(
            [FromQuery] int page = 1,
            [FromQuery] string? searchTerm = null,
            [FromQuery] Guid? warehouseId = null,
            [FromQuery] StorageZoneType? zoneType = null,
            [FromQuery] StorageZoneStatus? status = null,
            [FromQuery] bool? isDefault = null)
        {
            int size = _paginationSettings.EntitySizes.StorageZones;

            GetAllStorageZonesQuery query = new GetAllStorageZonesQuery
            {
                Page = page,
                Size = size,
                SearchTerm = searchTerm,
                WarehouseId = warehouseId,
                ZoneType = zoneType,
                Status = status,
                IsDefault = isDefault
            };

            return await mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<StorageZoneDto> GetStorageZoneByIdAsync(Guid id)
        {
            return await mediator.Send(new GetStorageZoneByIdQuery { Id = id });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPut("{id}")]
        public async Task<StorageZoneDto> UpdateStorageZoneAsync(Guid id, [FromBody] StorageZoneToCreateOrUpdateDto dto)
        {
            UpdateStorageZoneCommand command = dto.Adapt<UpdateStorageZoneCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [Authorize(Roles = "Owner")]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteStorageZoneAsync(Guid id)
        {
            return await mediator.Send(new DeleteStorageZoneCommand { Id = id });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPatch("{id}/status")]
        public async Task<bool> ChangeStorageZoneStatusAsync(Guid id, [FromBody] Domain.Enums.StorageZoneStatus status)
        {
            return await mediator.Send(new ChangeStorageZoneStatusCommand
            {
                Id = id,
                Status = status
            });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPatch("{id}/type")]
        public async Task<bool> ChangeStorageZoneTypeAsync(Guid id, [FromBody] Domain.Enums.StorageZoneType zoneType)
        {
            return await mediator.Send(new ChangeStorageZoneTypeCommand
            {
                Id = id,
                ZoneType = zoneType
            });
        }
    }
}