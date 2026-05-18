using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.Product
{
    public class GetProductByIdQuery : IRequest<ProductDto?>
    {
        public Guid Id { get; set; }
    }

    public class GetProductByIdQueryHandler(IProductRepository repository) : IRequestHandler<GetProductByIdQuery, ProductDto?>
    {
        public async Task<ProductDto?> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            ProductEntity? product = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Товар с ID {query.Id} не найден");
            
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                Article = product.Article,
                Description = product.Description,
                Attributes = product.Attributes,
                IsActive = product.IsActive,
                IsService = product.IsService
            };
        }
    }
}