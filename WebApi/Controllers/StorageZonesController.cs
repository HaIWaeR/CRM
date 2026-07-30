using Application.Behavior.StorageZone;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.StorageZone;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageZonesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateStorageZoneAsync([FromBody] StorageZoneToCreateOrUpdateDto dto)
        {
            CreateStorageZoneCommand command = dto.Adapt<CreateStorageZoneCommand>();
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<StorageZoneDto>> GetAllStorageZonesAsync(
            [FromQuery] string? searchTerm = null,
            [FromQuery] Guid? warehouseId = null,
            [FromQuery] Domain.Enums.StorageZoneType? zoneType = null,
            [FromQuery] Domain.Enums.StorageZoneStatus? status = null,
            [FromQuery] bool? isDefault = null)
        {
            GetAllStorageZonesQuery query = new GetAllStorageZonesQuery
            {
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

        [HttpPut("{id}")]
        public async Task<StorageZoneDto> UpdateStorageZoneAsync(Guid id, [FromBody] StorageZoneToCreateOrUpdateDto dto)
        {
            UpdateStorageZoneCommand command = dto.Adapt<UpdateStorageZoneCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteStorageZoneAsync(Guid id)
        {
            return await mediator.Send(new DeleteStorageZoneCommand { Id = id });
        }

        [HttpPatch("{id}/status")]
        public async Task<bool> ChangeStorageZoneStatusAsync(Guid id, [FromBody] Domain.Enums.StorageZoneStatus status)
        {
            return await mediator.Send(new ChangeStorageZoneStatusCommand
            {
                Id = id,
                Status = status
            });
        }

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