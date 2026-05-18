using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DTO;
using Application.Behavior.SupplierMaterial;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierMaterialsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateSupplierMaterialAsync([FromBody] CreateSupplierMaterialCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<SupplierMaterialDto>> GetAllSupplierMaterialsAsync()
        {
            return await mediator.Send(new GetAllSupplierMaterialsQuery());
        }

        [HttpGet("{id}")]
        public async Task<SupplierMaterialDto?> GetSupplierMaterialByIdAsync(Guid id)
        {
            return await mediator.Send(new GetSupplierMaterialByIdQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<SupplierMaterialDto?> UpdateSupplierMaterialAsync(Guid id, [FromBody] UpdateSupplierMaterialCommand command)
        {
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteSupplierMaterialAsync(Guid id)
        {
            return await mediator.Send(new DeleteSupplierMaterialCommand { Id = id });
        }
    }
}