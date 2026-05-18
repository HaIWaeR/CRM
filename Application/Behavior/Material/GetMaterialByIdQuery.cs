using Application.DTO;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behavior.Material
{
    public class GetMaterialByIdQuery : IRequest<MaterialDto?>
    {
        public Guid Id { get; set; }
    }

    public class GetMaterialByIdQueryHandler(IMaterialRepository repository) : IRequestHandler<GetMaterialByIdQuery, MaterialDto?>
    {
        public async Task<MaterialDto?> Handle(GetMaterialByIdQuery query, CancellationToken cancellationToken)
        {
            MaterialEntity? material = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Материал с ID {query.Id} не найден");

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
