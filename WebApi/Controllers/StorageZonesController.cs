using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.StorageZone;
using Application.Behavior.StorageZone;
using Domain.Entities;
using Mapster;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageZonesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateStorageZoneAsync([FromBody] CreateStorageZoneCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<StorageZoneDto>> GetAllStorageZonesAsync()
        {
            List<StorageZoneEntity> storageZones = await mediator.Send(new GetAllStorageZonesQuery());
            return storageZones.Adapt<List<StorageZoneDto>>();
        }

        [HttpGet("{id}")]
        public async Task<StorageZoneDto?> GetStorageZoneByIdAsync(Guid id)
        {
            StorageZoneEntity? storageZone = await mediator.Send(new GetStorageZoneByIdQuery { Id = id });
            return storageZone.Adapt<StorageZoneDto>();
        }

        [HttpPut("{id}")]
        public async Task<StorageZoneDto?> UpdateStorageZoneAsync(Guid id, [FromBody] UpdateStorageZoneCommand command)
        {
            command.Id = id;
            StorageZoneEntity? update = await mediator.Send(command);
            return update.Adapt<StorageZoneDto>();
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteStorageZoneAsync(Guid id)
        {
            return await mediator.Send(new DeleteStorageZoneCommand { Id = id });
        }
    }
}