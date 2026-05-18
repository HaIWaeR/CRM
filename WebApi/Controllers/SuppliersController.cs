using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DTO;
using Application.Behavior.Supplier;

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
            return await mediator.Send(new GetAllSuppliersQuery());
        }

        [HttpGet("{id}")]
        public async Task<SupplierDto?> GetSupplierByIdAsync(Guid id)
        {
            return await mediator.Send(new GetSupplierByIdQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<SupplierDto?> UpdateSupplierAsync(Guid id, [FromBody] UpdateSupplierCommand command)
        {
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteSupplierAsync(Guid id)
        {
            return await mediator.Send(new DeleteSupplierCommand { Id = id });
        }
    }
}