using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DTO;
using Application.Behavior.StorageZone;

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
            return await mediator.Send(new GetAllStorageZonesQuery());
        }

        [HttpGet("{id}")]
        public async Task<StorageZoneDto?> GetStorageZoneByIdAsync(Guid id)
        {
            return await mediator.Send(new GetStorageZoneByIdQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<StorageZoneDto?> UpdateStorageZoneAsync(Guid id, [FromBody] UpdateStorageZoneCommand command)
        {
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteStorageZoneAsync(Guid id)
        {
            return await mediator.Send(new DeleteStorageZoneCommand { Id = id });
        }
    }
}