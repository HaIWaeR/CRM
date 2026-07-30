using Application.Behavior.Supplier;
using Domain.Enums;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Supplier;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateSupplierAsync([FromBody] SupplierToCreateOrUpdateDto dto)
        {
            CreateSupplierCommand command = dto.Adapt<CreateSupplierCommand>();
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<SupplierDto>> GetAllSuppliersAsync(
            [FromQuery] string? searchTerm = null,
            [FromQuery] SupplierType? supplierType = null,
            [FromQuery] SupplierStatus? supplierStatus= null,
            [FromQuery] int? minRating = null,
            [FromQuery] int? maxRating = null)
        {
            GetAllSuppliersQuery query = new GetAllSuppliersQuery
            {
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

        [HttpPut("{id}")]
        public async Task<SupplierDto> UpdateSupplierAsync(Guid id, [FromBody] SupplierToCreateOrUpdateDto dto)
        {
            UpdateSupplierCommand command = dto.Adapt<UpdateSupplierCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteSupplierAsync(Guid id)
        {
            return await mediator.Send(new DeleteSupplierCommand { Id = id });
        }

        [HttpPatch("{id}/status")]
        public async Task<bool> ChangeSupplierStatusAsync(Guid id, [FromBody] SupplierStatus status)
        {
            return await mediator.Send(new ChangeSupplierStatusCommand
            {
                Id = id,
                Status = status
            });
        }

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