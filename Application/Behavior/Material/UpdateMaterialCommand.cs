using Application.Helpers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.Material;

namespace Application.Behavior.Material
{
    public class UpdateMaterialCommand : IRequest<MaterialDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Article { get; set; } = string.Empty;
        public string CategoryCode { get; set; } = "GEN";
        public decimal PriceUnit { get; set; }
        public decimal Weight { get; set; }
        public Domain.Enums.UnitMeasurement UnitMeasurement { get; set; }
        public string? Description { get; set; }
        public string? AdditionInformation { get; set; }
        public Dictionary<string, string>? Attributes { get; set; }
    }

    public class UpdateMaterialCommandHandler(IMaterialRepository repository) : IRequestHandler<UpdateMaterialCommand, MaterialDto>
    {
        public async Task<MaterialDto> Handle(UpdateMaterialCommand command, CancellationToken cancellationToken)
        {
            MaterialEntity? existing = await repository.GetByIdAsync(command.Id)
            ?? throw new KeyNotFoundException($"Материал с ID {command.Id} не найден");

            if (!string.IsNullOrWhiteSpace(command.Article))
            {
                if (!existing.Article.Equals(command.Article, StringComparison.OrdinalIgnoreCase))
                {
                    bool isUnique = await repository.IsArticleUniqueAsync(command.Article, command.Id);
                    if (!isUnique)
                        throw new InvalidOperationException($"Артикул '{command.Article}' уже существует");
                }
            }

            command.Adapt(existing);
            existing.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(existing);

            MaterialDto result = existing.Adapt<MaterialDto>();
            return result;
        }
    }
}
