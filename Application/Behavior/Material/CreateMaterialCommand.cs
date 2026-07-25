using Application.Helpers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Behavior.Material
{
    public class CreateMaterialCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Article { get; set; } = string.Empty;
        public string CategoryCode { get; set; } = "GEN";
        public int Quantity { get; set; }
        public decimal PriceUnit { get; set; }
        public decimal Weight { get; set; }
        public Domain.Enums.UnitMeasurement UnitMeasurement { get; set; }
        public string? Description { get; set; }
        public string? AdditionInformation { get; set; }
    }

    public class CreateMaterialCommandHandler(IMaterialRepository repository) : IRequestHandler<CreateMaterialCommand, Guid>
    {
        public async Task<Guid> Handle(CreateMaterialCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.Article))
            {
                command.Article = ArticleGenerator.Generate(command.Name, command.CategoryCode);
            }
            else
            {
                if (!await repository.IsArticleUniqueAsync(command.Article))
                    throw new InvalidOperationException($"Артикул '{command.Article}' уже существует");
            }

            MaterialEntity material = command.Adapt<MaterialEntity>();
            material.Id = Guid.NewGuid();
            material.CreatedAt = DateTime.UtcNow;
            material.IsActive = true;

            await repository.AddAsync(material);    
            return material.Id;
        }
    }
}
