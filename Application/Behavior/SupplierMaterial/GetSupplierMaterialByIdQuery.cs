using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.SupplierMaterial;

namespace Application.Behavior.SupplierMaterial
{
    public class GetSupplierMaterialByIdQuery : IRequest<SupplierMaterialDto>
    {
        public Guid Id { get; set; }
    }

    public class GetSupplierMaterialByIdQueryHandler(ISupplierMaterialRepository repository) : IRequestHandler<GetSupplierMaterialByIdQuery, SupplierMaterialDto>
    {
        public async Task<SupplierMaterialDto> Handle(GetSupplierMaterialByIdQuery query, CancellationToken cancellationToken)
        {
            SupplierMaterialEntity? supplierMaterial = await repository.GetByIdAsync(query.Id)
                ?? throw new KeyNotFoundException($"Запись с ID {query.Id} не найдена");

            SupplierMaterialDto result = supplierMaterial.Adapt<SupplierMaterialDto>();
            return result;
        }
    }
}