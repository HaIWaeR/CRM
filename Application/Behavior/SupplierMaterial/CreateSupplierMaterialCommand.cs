using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.SupplierMaterial
{
    public class CreateSupplierMaterialCommand : IRequest<Guid>
    {
        public Guid SupplierId { get; set; }
        public Guid MaterialId { get; set; }
        public decimal? Price { get; set; }
        public int? DeliveryDays { get; set; }
        public string? Note { get; set; }
    }

    public class CreateSupplierMaterialCommandHandler(ISupplierMaterialRepository repository) : IRequestHandler<CreateSupplierMaterialCommand, Guid>
    {
        public async Task<Guid> Handle(CreateSupplierMaterialCommand command, CancellationToken cancellationToken)
        {
            SupplierMaterialEntity entity = new SupplierMaterialEntity
            {
                Id = Guid.NewGuid(),
                SupplierId = command.SupplierId,
                MaterialId = command.MaterialId,
                Price = command.Price,
                DeliveryDays = command.DeliveryDays,
                Note = command.Note,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(entity);
            return entity.Id;
        }
    }
}