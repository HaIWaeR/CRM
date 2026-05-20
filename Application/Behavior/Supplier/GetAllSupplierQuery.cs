using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.Supplier
{
    public class GetAllSuppliersQuery : IRequest<List<SupplierEntity>>;

    public class GetAllSuppliersQueryHandler(ISupplierRepository repository) : IRequestHandler<GetAllSuppliersQuery, List<SupplierEntity>>
    {
        public async Task<List<SupplierEntity>> Handle(GetAllSuppliersQuery query, CancellationToken cancellationToken)
        {
            List<SupplierEntity> suppliers = await repository.GetAllAsync();

            return suppliers;
        }
    }
}