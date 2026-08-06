using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.Material;
using Shared.DTOs.Pagination;

namespace Application.Behavior.Material
{
    public class GetAllMaterialsQuery : IRequest<PaginatedResult<MaterialDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public string? SearchTerm { get; set; }
        public string? CategoryCode { get; set; }
        public MaterialStatus? Status { get; set; }
        public string? Article { get; set; }
    }

    public class GetAllMaterialsQueryHandler(IMaterialRepository repository) : IRequestHandler<GetAllMaterialsQuery, PaginatedResult<MaterialDto>>
    {
        public async Task<PaginatedResult<MaterialDto>> Handle(GetAllMaterialsQuery query, CancellationToken cancellationToken)
        {
            List<MaterialEntity> materials = await repository.GetFilteredAsync(
                query.SearchTerm,
                query.CategoryCode,
                query.Status,
                query.Article,
                query.Page,
                query.Size);

            int totalCount = await repository.GetTotalCountAsync(
                query.SearchTerm,
                query.CategoryCode,
                query.Status,
                query.Article);

            int totalPages = (int)Math.Ceiling(totalCount / (double)query.Size);

            if (query.Page > totalPages && totalPages > 0)
            {
                return new PaginatedResult<MaterialDto>
                {
                    Items = new List<MaterialDto>(),
                    TotalCount = totalCount,
                    Page = query.Page,
                    Size = query.Size,
                    TotalPages = totalPages
                };
            }

            List<MaterialDto> items = materials.Adapt<List<MaterialDto>>();

            return new PaginatedResult<MaterialDto>
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