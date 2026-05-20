using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.SupplierMaterial
{
    public class GetSupplierMaterialByIdQuery : IRequest<SupplierMaterialEntity?>
    {
        public Guid Id { get; set; }
    }

    public class GetSupplierMaterialByIdQueryHandler(ISupplierMaterialRepository repository) : IRequestHandler<GetSupplierMaterialByIdQuery, SupplierMaterialEntity?>
    {
        public async Task<SupplierMaterialEntity?> Handle(GetSupplierMaterialByIdQuery query, CancellationToken cancellationToken)
        {
            SupplierMaterialEntity? item = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Связь с ID {query.Id} не найдена");

            return item;
        }
    }
}