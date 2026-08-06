using Application.Behavior.SupplierMaterial;
using Domain.Settings;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.DTOs.Pagination;
using Shared.DTOs.SupplierMaterial;

namespace WebApi.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierMaterialsController(IMediator mediator, IOptions<PaginationSettings> paginationOptions) : ControllerBase
    {
        private readonly PaginationSettings _paginationSettings = paginationOptions.Value;
        [Authorize(Roles = "Owner, Admin")]
        [HttpPost]
        public async Task<Guid> CreateSupplierMaterialAsync([FromBody] SupplierMaterialToCreateOrUpdateDto dto)
        {
            CreateSupplierMaterialCommand command = dto.Adapt<CreateSupplierMaterialCommand>();
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<PaginatedResult<SupplierMaterialDto>> GetAllSupplierMaterialsAsync(
            [FromQuery] int page = 1,
            [FromQuery] Guid? supplierId = null,
            [FromQuery] Guid? materialId = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] int? maxDeliveryDays = null)
        {
            int size = _paginationSettings.EntitySizes.SupplierMaterials;

            GetAllSupplierMaterialsQuery query = new GetAllSupplierMaterialsQuery
            {
                Page = page,
                Size = size,
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

        [Authorize(Roles = "Owner, Admin")]
        [HttpPut("{id}")]
        public async Task<SupplierMaterialDto> UpdateSupplierMaterialAsync(Guid id, [FromBody] SupplierMaterialToCreateOrUpdateDto dto)
        {
            UpdateSupplierMaterialCommand command = dto.Adapt<UpdateSupplierMaterialCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [Authorize(Roles = "Owner")]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteSupplierMaterialAsync(Guid id)
        {
            return await mediator.Send(new DeleteSupplierMaterialCommand { Id = id });
        }
    }
}