using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.Supplier
{
    public class GetSupplierByIdQuery : IRequest<SupplierDto?>
    {
        public Guid Id { get; set; }
    }

    public class GetSupplierByIdQueryHandler(ISupplierRepository repository) : IRequestHandler<GetSupplierByIdQuery, SupplierDto?>
    {
        public async Task<SupplierDto?> Handle(GetSupplierByIdQuery query, CancellationToken cancellationToken)
        {
            SupplierEntity? supplier = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Поставщик с ID {query.Id} не найден");
            return new SupplierDto
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
            };
        }
    }
}