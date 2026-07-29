using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Behavior.SupplierMaterial
{
    public class CreateSupplierMaterialCommand : IRequest<Guid>
    {
        public Guid SupplierId { get; set; }
        public Guid MaterialId { get; set; }
        public decimal? PriceUnit { get; set; }
        public int? DeliveryDays { get; set; }
        public string? Description { get; set; }
    }

    public class CreateSupplierMaterialCommandHandler(ISupplierMaterialRepository repository) : IRequestHandler<CreateSupplierMaterialCommand, Guid>
    {
        public async Task<Guid> Handle(CreateSupplierMaterialCommand command, CancellationToken cancellationToken)
        {
            SupplierMaterialEntity supplierMaterial = command.Adapt<SupplierMaterialEntity>();
            supplierMaterial.Id = Guid.NewGuid();
            supplierMaterial.CreatedAt = DateTime.UtcNow;

            await repository.AddAsync(supplierMaterial);
            return supplierMaterial.Id;
        }
    }
}