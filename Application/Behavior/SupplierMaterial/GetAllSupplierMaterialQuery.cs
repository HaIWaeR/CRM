using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.SupplierMaterial;
using Shared.DTOs.Pagination;

namespace Application.Behavior.SupplierMaterial
{
    public class GetAllSupplierMaterialsQuery : IRequest<PaginatedResult<SupplierMaterialDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public Guid? SupplierId { get; set; }
        public Guid? MaterialId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MaxDeliveryDays { get; set; }
    }

    public class GetAllSupplierMaterialsQueryHandler(ISupplierMaterialRepository repository) : IRequestHandler<GetAllSupplierMaterialsQuery, PaginatedResult<SupplierMaterialDto>>
    {
        public async Task<PaginatedResult<SupplierMaterialDto>> Handle(GetAllSupplierMaterialsQuery query, CancellationToken cancellationToken)
        {
            List<SupplierMaterialEntity> supplierMaterials = await repository.GetFilteredAsync(
                query.SupplierId,
                query.MaterialId,
                query.MinPrice,
                query.MaxPrice,
                query.MaxDeliveryDays,
                query.Page,
                query.Size);

            int totalCount = await repository.GetTotalCountAsync(
                query.SupplierId,
                query.MaterialId,
                query.MinPrice,
                query.MaxPrice,
                query.MaxDeliveryDays);

            int totalPages = (int)Math.Ceiling(totalCount / (double)query.Size);

            if (query.Page > totalPages && totalPages > 0)
            {
                return new PaginatedResult<SupplierMaterialDto>
                {
                    Items = new List<SupplierMaterialDto>(),
                    TotalCount = totalCount,
                    Page = query.Page,
                    Size = query.Size,
                    TotalPages = totalPages
                };
            }

            List<SupplierMaterialDto> items = supplierMaterials.Adapt<List<SupplierMaterialDto>>();

            return new PaginatedResult<SupplierMaterialDto>
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