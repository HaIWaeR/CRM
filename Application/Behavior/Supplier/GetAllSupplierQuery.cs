using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.Supplier;
using Shared.DTOs.Pagination;

namespace Application.Behavior.Supplier
{
    public class GetAllSuppliersQuery : IRequest<PaginatedResult<SupplierDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public string? SearchTerm { get; set; }
        public SupplierType? SupplierType { get; set; }
        public SupplierStatus? SupplierStatus { get; set; }
        public int? MinRating { get; set; }
        public int? MaxRating { get; set; }
    }

    public class GetAllSuppliersQueryHandler(ISupplierRepository repository) : IRequestHandler<GetAllSuppliersQuery, PaginatedResult<SupplierDto>>
    {
        public async Task<PaginatedResult<SupplierDto>> Handle(GetAllSuppliersQuery query, CancellationToken cancellationToken)
        {
            List<SupplierEntity> suppliers = await repository.GetFilteredAsync(
                query.SearchTerm,
                query.SupplierType,
                query.SupplierStatus,
                query.MinRating,
                query.MaxRating,
                query.Page,
                query.Size);

            int totalCount = await repository.GetTotalCountAsync(
                query.SearchTerm,
                query.SupplierType,
                query.SupplierStatus,
                query.MinRating,
                query.MaxRating);

            int totalPages = (int)Math.Ceiling(totalCount / (double)query.Size);

            if (query.Page > totalPages && totalPages > 0)
            {
                return new PaginatedResult<SupplierDto>
                {
                    Items = new List<SupplierDto>(),
                    TotalCount = totalCount,
                    Page = query.Page,
                    Size = query.Size,
                    TotalPages = totalPages
                };
            }

            List<SupplierDto> items = suppliers.Adapt<List<SupplierDto>>();

            return new PaginatedResult<SupplierDto>
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