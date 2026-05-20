using Application.Behavior.Supplier;
using Application.Behavior.SupplierMaterial;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.SupplierMaterial;

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
            List<SupplierMaterialEntity> supplierMaterials = await mediator.Send(new GetAllSupplierMaterialsQuery());
            return supplierMaterials.Adapt<List<SupplierMaterialDto>>();
        }

        [HttpGet("{id}")]
        public async Task<SupplierMaterialDto?> GetSupplierMaterialByIdAsync(Guid id)
        {
            SupplierMaterialEntity? supplierMaterial = await mediator.Send(new GetSupplierMaterialByIdQuery { Id = id });
            return supplierMaterial.Adapt<SupplierMaterialDto>();
        }

        [HttpPut("{id}")]
        public async Task<SupplierMaterialDto?> UpdateSupplierMaterialAsync(Guid id, [FromBody] UpdateSupplierMaterialCommand command)
        {
            command.Id = id;
            SupplierMaterialEntity? update = await mediator.Send(command);
            return update.Adapt<SupplierMaterialDto>();
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteSupplierMaterialAsync(Guid id)
        {
            return await mediator.Send(new DeleteSupplierMaterialCommand { Id = id });
        }
    }
}