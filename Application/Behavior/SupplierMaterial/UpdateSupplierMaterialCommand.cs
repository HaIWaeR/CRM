using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.SupplierMaterial
{
    public class UpdateSupplierMaterialCommand : IRequest<SupplierMaterialDto?>
    {
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public Guid MaterialId { get; set; }
        public decimal? Price { get; set; }
        public int? DeliveryDays { get; set; }
        public string? Note { get; set; }
    }

    public class UpdateSupplierMaterialCommandHandler(ISupplierMaterialRepository repository) : IRequestHandler<UpdateSupplierMaterialCommand, SupplierMaterialDto?>
    {
        public async Task<SupplierMaterialDto?> Handle(UpdateSupplierMaterialCommand command, CancellationToken cancellationToken)
        {
            SupplierMaterialEntity? item = await repository.GetByIdAsync(command.Id) ?? throw new Exception($"Связь с ID {command.Id} не найдена");
            item.SupplierId = command.SupplierId;
            item.MaterialId = command.MaterialId;
            item.Price = command.Price;
            item.DeliveryDays = command.DeliveryDays;
            item.Note = command.Note;
            item.UpdatedAt = DateTime.UtcNow;
            await repository.UpdateAsync(item);
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