using Application.Behavior.Material;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Material;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateMaterialAsync([FromBody] CreateMaterialCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<MaterialDto>> GetAllMaterialsAsync(
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? categoryCode = null,
            [FromQuery] bool? isActive = null,
            [FromQuery] string? article = null)
        {
            GetAllMaterialsQuery query = new GetAllMaterialsQuery
            {
                SearchTerm = searchTerm,
                CategoryCode = categoryCode,
                IsActive = isActive,
                Article = article
            };

            return await mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<MaterialDto?> GetMaterialByIdAsync(Guid id)
        {
            
            return await mediator.Send(new GetMaterialByIdQuery { Id = id });

        }

        [HttpPut("{id}")]
        public async Task<MaterialDto> UpdateMaterialAsync(Guid id, [FromBody] MaterialToCreateOrUpdateDto dto)
        {
            UpdateMaterialCommand command = dto.Adapt<UpdateMaterialCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteMaterialAsync(Guid id)
        {
            return await mediator.Send(new DeleteMaterialCommand { Id = id });
        }

        [HttpPatch("{id}/status")]
        public async Task<bool> ChangeMaterialStatusAsync(Guid id, [FromBody] bool isActive)
        {
            return await mediator.Send(new ChangeMaterialStatusCommand
            {
                Id = id,
                IsActive = isActive
            });
        }
    }
}