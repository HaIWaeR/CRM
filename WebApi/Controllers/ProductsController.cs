using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.Product;
using Application.Behavior.Product;
using Domain.Entities;
using Mapster;

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
            List<ProductEntity> products = await mediator.Send(new GetAllProductsQuery());
            return products.Adapt<List<ProductDto>>();
        }

        [HttpGet("{id}")]
        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            ProductEntity? product = await mediator.Send(new GetProductByIdQuery { Id = id });
            return product.Adapt<ProductDto>();
        }

        [HttpPut("{id}")]
        public async Task<ProductDto?> UpdateProductAsync(Guid id, [FromBody] UpdateProductCommand command)
        {
            command.Id = id;
            ProductEntity? update = await mediator.Send(command);
            return update.Adapt<ProductDto>();
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteProductAsync(Guid id)
        {
            return await mediator.Send(new DeleteProductCommand { Id = id });
        }
    }
}