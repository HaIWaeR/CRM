using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DTO;
using Application.Behavior.Product;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateProductAsync([FromBody] CreateProductCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            return await mediator.Send(new GetAllProductsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            return await mediator.Send(new GetProductByIdQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<ProductDto?> UpdateProductAsync(Guid id, [FromBody] UpdateProductCommand command)
        {
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteProductAsync(Guid id)
        {
            return await mediator.Send(new DeleteProductCommand { Id = id });
        }
    }
}