using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.Product
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>;

    public class GetAllProductsQueryHandler(IProductRepository repository) : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        public async Task<List<ProductDto>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            List<ProductEntity> products = await repository.GetAllAsync();
            
            return products.Select(product => new ProductDto
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
            }).ToList();
        }
    }
}