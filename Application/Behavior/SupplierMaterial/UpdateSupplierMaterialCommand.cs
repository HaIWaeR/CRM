using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.SupplierMaterial;

namespace Application.Behavior.SupplierMaterial
{
    public class UpdateSupplierMaterialCommand : IRequest<SupplierMaterialDto>
    {
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public Guid MaterialId { get; set; }
        public decimal? PriceUnit { get; set; }
        public int? DeliveryDays { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateSupplierMaterialCommandHandler(ISupplierMaterialRepository repository) : IRequestHandler<UpdateSupplierMaterialCommand, SupplierMaterialDto>
    {
        public async Task<SupplierMaterialDto> Handle(UpdateSupplierMaterialCommand command, CancellationToken cancellationToken)
        {
            SupplierMaterialEntity? existing = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Запись с ID {command.Id} не найдена");

            command.Adapt(existing);
            existing.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(existing);

            SupplierMaterialDto result = existing.Adapt<SupplierMaterialDto>();
            return result;
        }
    }
}