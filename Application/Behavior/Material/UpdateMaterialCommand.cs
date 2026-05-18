using Application.DTO;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Material
{
    public class UpdateMaterialCommand : IRequest<MaterialDto?>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Article { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PriceUnit { get; set; }
        public UnitMeasurement UnitMeasurement { get; set; }
        public string? Description { get; set; }
        public string? CellZone { get; set; }
        public string? AdditionInforamtion { get; set; }
    }

    public class UpdateMaterialCommandHandler(IMaterialRepository repository) : IRequestHandler<UpdateMaterialCommand, MaterialDto?>
    {
        public async Task<MaterialDto?> Handle(UpdateMaterialCommand command, CancellationToken cancellationToken)
        {
            MaterialEntity? material = await repository.GetByIdAsync(command.Id) ?? throw new Exception($"Материал с ID {command.Id} не найден");

            material.Name = command.Name;
            material.Article = command.Article;
            material.Quantity = command.Quantity;
            material.PriceUnit = command.PriceUnit;
            material.UnitMeasurement = command.UnitMeasurement;
            material.Description = command.Description;
            material.CellZone = command.CellZone;
            material.AdditionInforamtion = command.AdditionInforamtion;
            material.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(material);

            return new MaterialDto
            {
                Id = material.Id,
                Name = material.Name,
                Article = material.Article,
                Quantity = material.Quantity,
                PriceUnit = material.PriceUnit,
                UnitMeasurement = material.UnitMeasurement,
                Description = material.Description,
                CellZone = material.CellZone,
                AdditionInforamtion = material.AdditionInforamtion
            };
        }
    }

}
