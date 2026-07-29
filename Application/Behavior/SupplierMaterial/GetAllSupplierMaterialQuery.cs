using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.SupplierMaterial;

namespace Application.Behavior.SupplierMaterial
{
    public class GetAllSupplierMaterialsQuery : IRequest<List<SupplierMaterialDto>>
    {
        public Guid? SupplierId { get; set; }
        public Guid? MaterialId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MaxDeliveryDays { get; set; }
    }

    public class GetAllSupplierMaterialsQueryHandler(ISupplierMaterialRepository repository) : IRequestHandler<GetAllSupplierMaterialsQuery, List<SupplierMaterialDto>>
    {
        public async Task<List<SupplierMaterialDto>> Handle(GetAllSupplierMaterialsQuery query, CancellationToken cancellationToken)
        {
            List<SupplierMaterialEntity> supplierMaterials = await repository.GetFilteredAsync(
                query.SupplierId,
                query.MaterialId,
                query.MinPrice,
                query.MaxPrice,
                query.MaxDeliveryDays);

            List<SupplierMaterialDto> result = supplierMaterials.Adapt<List<SupplierMaterialDto>>();
            return result;
        }
    }
}