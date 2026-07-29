using Application.Behavior.SupplierMaterial;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.SupplierMaterial;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierMaterialsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateSupplierMaterialAsync([FromBody] SupplierMaterialToCreateOrUpdateDto dto)
        {
            CreateSupplierMaterialCommand command = dto.Adapt<CreateSupplierMaterialCommand>();
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<SupplierMaterialDto>> GetAllSupplierMaterialsAsync(
            [FromQuery] Guid? supplierId = null,
            [FromQuery] Guid? materialId = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] int? maxDeliveryDays = null)
        {
            GetAllSupplierMaterialsQuery query = new GetAllSupplierMaterialsQuery
            {
                SupplierId = supplierId,
                MaterialId = materialId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                MaxDeliveryDays = maxDeliveryDays
            };

            return await mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<SupplierMaterialDto> GetSupplierMaterialByIdAsync(Guid id)
        {
            return await mediator.Send(new GetSupplierMaterialByIdQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<SupplierMaterialDto> UpdateSupplierMaterialAsync(Guid id, [FromBody] SupplierMaterialToCreateOrUpdateDto dto)
        {
            UpdateSupplierMaterialCommand command = dto.Adapt<UpdateSupplierMaterialCommand>();
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