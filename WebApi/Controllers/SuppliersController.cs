using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.Supplier;
using Application.Behavior.Supplier;
using Domain.Entities;
using Mapster;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateSupplierAsync([FromBody] CreateSupplierCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<SupplierDto>> GetAllSuppliersAsync()
        {
            List<SupplierEntity> suppliers = await mediator.Send(new GetAllSuppliersQuery());
            return suppliers.Adapt<List<SupplierDto>>();
        }

        [HttpGet("{id}")]
        public async Task<SupplierDto?> GetSupplierByIdAsync(Guid id)
        {
            SupplierEntity? supplier = await mediator.Send(new GetSupplierByIdQuery { Id = id });
            return supplier.Adapt<SupplierDto>();
        }

        [HttpPut("{id}")]
        public async Task<SupplierDto?> UpdateSupplierAsync(Guid id, [FromBody] UpdateSupplierCommand command)
        {
            command.Id = id;
            SupplierEntity? update = await mediator.Send(command);
            return update.Adapt<SupplierDto>();
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteSupplierAsync(Guid id)
        {
            return await mediator.Send(new DeleteSupplierCommand { Id = id });
        }
    }
}