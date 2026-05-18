using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.SupplierMaterial
{
    public class GetSupplierMaterialByIdQuery : IRequest<SupplierMaterialDto?>
    {
        public Guid Id { get; set; }
    }

    public class GetSupplierMaterialByIdQueryHandler(ISupplierMaterialRepository repository) : IRequestHandler<GetSupplierMaterialByIdQuery, SupplierMaterialDto?>
    {
        public async Task<SupplierMaterialDto?> Handle(GetSupplierMaterialByIdQuery query, CancellationToken cancellationToken)
        {
            SupplierMaterialEntity? item = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Связь с ID {query.Id} не найдена");
            return new SupplierMaterialDto
            {
                Id = item.Id,
                SupplierId = item.SupplierId,
                MaterialId = item.MaterialId,
                Price = item.Price,
                DeliveryDays = item.DeliveryDays,
                Note = item.Note,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt
            };
        }
    }
}