using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.Supplier;

namespace Application.Behavior.Supplier
{
    public class GetSupplierByIdQuery : IRequest<SupplierDto>
    {
        public Guid Id { get; set; }
    }

    public class GetSupplierByIdQueryHandler(ISupplierRepository repository) : IRequestHandler<GetSupplierByIdQuery, SupplierDto>
    {
        public async Task<SupplierDto> Handle(GetSupplierByIdQuery query, CancellationToken cancellationToken)
        {
            SupplierEntity? supplier = await repository.GetByIdAsync(query.Id)
                ?? throw new KeyNotFoundException($"Поставщик с ID {query.Id} не найден");

            SupplierDto result = supplier.Adapt<SupplierDto>();
            return result;
        }
    }
}