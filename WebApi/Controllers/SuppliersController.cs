using Application.Behavior.Supplier;
using Domain.Enums;
using Domain.Settings;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.DTOs.Pagination;
using Shared.DTOs.Supplier;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController(IMediator mediator, IOptions<PaginationSettings> paginationOptions) : ControllerBase
    {
        private readonly PaginationSettings _paginationSettings = paginationOptions.Value;
        [Authorize(Roles = "Owner, Admin")]
        [HttpPost]
        public async Task<Guid> CreateSupplierAsync([FromBody] SupplierToCreateOrUpdateDto dto)
        {
            CreateSupplierCommand command = dto.Adapt<CreateSupplierCommand>();
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<PaginatedResult<SupplierDto>> GetAllSuppliersAsync(
            [FromQuery] int page = 1,
            [FromQuery] string? searchTerm = null,
            [FromQuery] SupplierType? supplierType = null,
            [FromQuery] SupplierStatus? supplierStatus = null,
            [FromQuery] int? minRating = null,
            [FromQuery] int? maxRating = null)
        {
            int size = _paginationSettings.EntitySizes.Suppliers;

            GetAllSuppliersQuery query = new GetAllSuppliersQuery
            {
                Page = page,
                Size = size,
                SearchTerm = searchTerm,
                SupplierType = supplierType,
                SupplierStatus = supplierStatus,
                MinRating = minRating,
                MaxRating = maxRating
            };

            return await mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<SupplierDto> GetSupplierByIdAsync(Guid id)
        {
            return await mediator.Send(new GetSupplierByIdQuery { Id = id });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPut("{id}")]
        public async Task<SupplierDto> UpdateSupplierAsync(Guid id, [FromBody] SupplierToCreateOrUpdateDto dto)
        {
            UpdateSupplierCommand command = dto.Adapt<UpdateSupplierCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteSupplierAsync(Guid id)
        {
            return await mediator.Send(new DeleteSupplierCommand { Id = id });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPatch("{id}/status")]
        public async Task<bool> ChangeSupplierStatusAsync(Guid id, [FromBody] SupplierStatus status)
        {
            return await mediator.Send(new ChangeSupplierStatusCommand
            {
                Id = id,
                Status = status
            });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPatch("{id}/type")]
        public async Task<bool> ChangeSupplierTypeAsync(Guid id, [FromBody] SupplierType supplierType)
        {
            return await mediator.Send(new ChangeSupplierTypeCommand
            {
                Id = id,
                SupplierType = supplierType
            });
        }
    }
}