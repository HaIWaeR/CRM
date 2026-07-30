using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using WebApi.DTO.Product;

namespace Application.Behavior.Product
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
        public string? SearchTerm { get; set; }
        public string? Category { get; set; }
        public ProductStatus? Status { get; set; }
        public bool? IsService { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }

    public class GetAllProductsQueryHandler(IProductRepository repository) : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        public async Task<List<ProductDto>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            List<ProductEntity> products = await repository.GetFilteredAsync(
                query.SearchTerm,
                query.Category,
                query.Status,
                query.IsService,
                query.MinPrice,
                query.MaxPrice);

            List<ProductDto> result = products.Adapt<List<ProductDto>>();
            return result;
        }
    }
}