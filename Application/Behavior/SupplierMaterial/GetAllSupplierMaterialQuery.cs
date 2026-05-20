using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.SupplierMaterial
{
    public class GetAllSupplierMaterialsQuery : IRequest<List<SupplierMaterialEntity>>;

    public class GetAllSupplierMaterialsQueryHandler(ISupplierMaterialRepository repository) : IRequestHandler<GetAllSupplierMaterialsQuery, List<SupplierMaterialEntity>>
    {
        public async Task<List<SupplierMaterialEntity>> Handle(GetAllSupplierMaterialsQuery query, CancellationToken cancellationToken)
        {
            List<SupplierMaterialEntity> items = await repository.GetAllAsync();

            return items;
        }
    }
}