using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Material
{
    public class UpdateMaterialCommand : IRequest<MaterialEntity?>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CategoryCode { get; set; } = "GEN";
        public int Quantity { get; set; }
        public decimal PriceUnit { get; set; }
        public UnitMeasurement UnitMeasurement { get; set; }
        public string? Description { get; set; }
        public string? CellZone { get; set; }
        public string? AdditionInforamtion { get; set; }
    }

    public class UpdateMaterialCommandHandler(IMaterialRepository repository) : IRequestHandler<UpdateMaterialCommand, MaterialEntity?>
    {
        public async Task<MaterialEntity?> Handle(UpdateMaterialCommand command, CancellationToken cancellationToken)
        {
            MaterialEntity? material = await repository.GetByIdAsync(command.Id)
                ?? throw new Exception($"Материал с ID {command.Id} не найден");

            if (command.CategoryCode != material.CategoryCode)
            {
                string prefix = command.CategoryCode.ToUpper();
                string? lastArticle = await repository.GetLastArticleByCategoryAsync(prefix);

                int nextNumber = 1;
                if (lastArticle != null)
                {
                    string lastNumberPart = lastArticle.Split('-').Last();
                    if (int.TryParse(lastNumberPart, out int parsedNumber))
                    {
                        nextNumber = parsedNumber + 1;
                    }
                }
                material.Article = $"{prefix}-{nextNumber:D3}";
                material.CategoryCode = command.CategoryCode;
            }

            material.Name = command.Name;
            material.Quantity = command.Quantity;
            material.PriceUnit = command.PriceUnit;
            material.UnitMeasurement = command.UnitMeasurement;
            material.Description = command.Description;
            material.CellZone = command.CellZone;
            material.AdditionInforamtion = command.AdditionInforamtion;
            material.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(material);

            return material;
        }
    }
}
