using Application.Behavior.Product;
using Domain.Enums;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.Product;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        [Authorize(Roles = "Owner, Admin")]
        [HttpPost]
        public async Task<Guid> CreateProductAsync([FromBody] ProductToCreateOrUpdateDto dto)
        {
            CreateProductCommand command = dto.Adapt<CreateProductCommand>();
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<ProductDto>> GetAllProductsAsync(
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? category = null,
            [FromQuery] ProductStatus? status = null,
            [FromQuery] bool? isService = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null)
        {
            GetAllProductsQuery query = new GetAllProductsQuery
            {
                SearchTerm = searchTerm,
                Category = category,
                Status = status,
                IsService = isService,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };

            return await mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            return await mediator.Send(new GetProductByIdQuery { Id = id });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPut("{id}")]
        public async Task<ProductDto> UpdateProductAsync(Guid id, [FromBody] ProductToCreateOrUpdateDto dto)
        {
            UpdateProductCommand command = dto.Adapt<UpdateProductCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteProductAsync(Guid id)
        {
            return await mediator.Send(new DeleteProductCommand { Id = id });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPatch("{id}/status")]
        public async Task<bool> ChangeProductStatusAsync(Guid id, [FromBody] ProductStatus status)
        {
            return await mediator.Send(new ChangeProductStatusCommand
            {
                Id = id,
                Status = status
            });
        }
    }
}