using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.SupplierMaterial
{
    public class GetAllSupplierMaterialsQuery : IRequest<List<SupplierMaterialDto>>;

    public class GetAllSupplierMaterialsQueryHandler(ISupplierMaterialRepository repository) : IRequestHandler<GetAllSupplierMaterialsQuery, List<SupplierMaterialDto>>
    {
        public async Task<List<SupplierMaterialDto>> Handle(GetAllSupplierMaterialsQuery query, CancellationToken cancellationToken)
        {
            List<SupplierMaterialEntity> items = await repository.GetAllAsync();
            return items.Select(item => new SupplierMaterialDto
            {
                Id = item.Id,
                SupplierId = item.SupplierId,
                MaterialId = item.MaterialId,
                Price = item.Price,
                DeliveryDays = item.DeliveryDays,
                Note = item.Note,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt
            }).ToList();
        }
    }
}