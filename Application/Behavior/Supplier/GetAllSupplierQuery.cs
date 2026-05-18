using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.Supplier
{
    public class GetAllSuppliersQuery : IRequest<List<SupplierDto>>;

    public class GetAllSuppliersQueryHandler(ISupplierRepository repository) : IRequestHandler<GetAllSuppliersQuery, List<SupplierDto>>
    {
        public async Task<List<SupplierDto>> Handle(GetAllSuppliersQuery query, CancellationToken cancellationToken)
        {
            List<SupplierEntity> suppliers = await repository.GetAllAsync();
            return suppliers.Select(supplier => new SupplierDto
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Inn = supplier.Inn,
                Kpp = supplier.Kpp,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                Website = supplier.Website,
                BankDetails = supplier.BankDetails,
                Description = supplier.Description,
                IsActive = supplier.IsActive,
                CreatedAt = supplier.CreatedAt,
                UpdatedAt = supplier.UpdatedAt
            }).ToList();
        }
    }
}