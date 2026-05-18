using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Material
{
    public class CreateMaterialCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Article { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PriceUnit { get; set; }
        public UnitMeasurement UnitMeasurement { get; set; }
        public string? Description { get; set; }
        public string? CellZone { get; set; }
        public string? AdditionInforamtion { get; set; }
    }

    public class CreateMaterialCommandHandler(IMaterialRepository repository) : IRequestHandler<CreateMaterialCommand, Guid>
    {
        public async Task<Guid> Handle(CreateMaterialCommand command, CancellationToken cancellationToken)
        {
            MaterialEntity material = new MaterialEntity
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Article = command.Article,
                Quantity = command.Quantity,
                PriceUnit = command.PriceUnit,
                UnitMeasurement = command.UnitMeasurement,
                Description = command.Description,
                CellZone = command.CellZone,
                AdditionInforamtion = command.AdditionInforamtion,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(material);
            return material.Id;
        }
    }




}
