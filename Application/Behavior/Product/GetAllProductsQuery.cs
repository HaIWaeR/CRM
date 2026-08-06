using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.Pagination;
using WebApi.DTO.Product;

namespace Application.Behavior.Product
{
    public class GetAllProductsQuery : IRequest<PaginatedResult<ProductDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public string? SearchTerm { get; set; }
        public string? Category { get; set; }
        public ProductStatus? Status { get; set; }
        public bool? IsService { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }

    public class GetAllProductsQueryHandler(IProductRepository repository) : IRequestHandler<GetAllProductsQuery, PaginatedResult<ProductDto>>
    {
        public async Task<PaginatedResult<ProductDto>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            List<ProductEntity> products = await repository.GetFilteredAsync(
                query.SearchTerm,
                query.Category,
                query.Status,
                query.IsService,
                query.MinPrice,
                query.MaxPrice,
                query.Page,
                query.Size);

            int totalCount = await repository.GetTotalCountAsync(
                query.SearchTerm,
                query.Category,
                query.Status,
                query.IsService,
                query.MinPrice,
                query.MaxPrice);

            int totalPages = (int)Math.Ceiling(totalCount / (double)query.Size);

            if (query.Page > totalPages && totalPages > 0)
            {
                return new PaginatedResult<ProductDto>
                {
                    Items = new List<ProductDto>(),
                    TotalCount = totalCount,
                    Page = query.Page,
                    Size = query.Size,
                    TotalPages = totalPages
                };
            }

            List<ProductDto> items = products.Adapt<List<ProductDto>>();

            return new PaginatedResult<ProductDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = query.Page,
                Size = query.Size,
                TotalPages = totalPages
            };
        }
    }
}