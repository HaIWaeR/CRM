using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.Supplier
{
    public class GetSupplierByIdQuery : IRequest<SupplierEntity?>
    {
        public Guid Id { get; set; }
    }

    public class GetSupplierByIdQueryHandler(ISupplierRepository repository) : IRequestHandler<GetSupplierByIdQuery, SupplierEntity?>
    {
        public async Task<SupplierEntity?> Handle(GetSupplierByIdQuery query, CancellationToken cancellationToken)
        {
            SupplierEntity? supplier = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Поставщик с ID {query.Id} не найден");

            return supplier;
        }
    }
}