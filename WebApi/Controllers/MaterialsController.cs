using Application.Behavior.Material;
using WebApi.DTO.Material;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Mapster;

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
        public async Task<List<MaterialDto>> GetAllMaterialsAsync()
        {
            List<MaterialEntity> materials = await mediator.Send(new GetAllMaterialsQuery());
            return materials.Adapt<List<MaterialDto>>();

        }

        [HttpGet("{id}")]
        public async Task<MaterialDto?> GetMaterialByIdAsync(Guid id)
        {
            MaterialEntity? material = await mediator.Send(new GetMaterialByIdQuery { Id = id });
            return material.Adapt<MaterialDto>();

        }

        [HttpPut("{id}")]
        public async Task<MaterialDto?> UpdateMaterialAsync(Guid id, [FromBody] UpdateMaterialCommand command)
        {
            command.Id = id;
            MaterialEntity? update = await mediator.Send(command);
            return update.Adapt<MaterialDto>();
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteMaterialAsync(Guid id)
        {
            return await mediator.Send(new DeleteMaterialCommand { Id = id });
        }
    }
}