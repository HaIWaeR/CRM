using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.SupplierMaterial
{
    public class UpdateSupplierMaterialCommand : IRequest<SupplierMaterialEntity?>
    {
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public Guid MaterialId { get; set; }
        public decimal? Price { get; set; }
        public int? DeliveryDays { get; set; }
        public string? Note { get; set; }
    }

    public class UpdateSupplierMaterialCommandHandler(ISupplierMaterialRepository repository) : IRequestHandler<UpdateSupplierMaterialCommand, SupplierMaterialEntity?>
    {
        public async Task<SupplierMaterialEntity?> Handle(UpdateSupplierMaterialCommand command, CancellationToken cancellationToken)
        {
            SupplierMaterialEntity? item = await repository.GetByIdAsync(command.Id) ?? throw new Exception($"Связь с ID {command.Id} не найдена");
            item.SupplierId = command.SupplierId;
            item.MaterialId = command.MaterialId;
            item.Price = command.Price;
            item.DeliveryDays = command.DeliveryDays;
            item.Note = command.Note;
            item.UpdatedAt = DateTime.UtcNow;
            await repository.UpdateAsync(item);

            return item;
        }
    }
}