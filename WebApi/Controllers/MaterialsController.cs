using Application.Behavior.Material;
using Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            return await mediator.Send(new GetAllMaterialsQuery());
        }

        [HttpGet("{id}")]
        public async Task<MaterialDto?> GetMaterialByIdAsync(Guid id)
        {
            return await mediator.Send(new GetMaterialByIdQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<MaterialDto?> UpdateMaterialAsync(Guid id, [FromBody] UpdateMaterialCommand command)
        {
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteMaterialAsync(Guid id)
        {
            return await mediator.Send(new DeleteMaterialCommand { Id = id });
        }
    }
}