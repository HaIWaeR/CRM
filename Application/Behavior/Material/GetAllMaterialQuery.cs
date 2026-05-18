using Application.DTO;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behavior.Material
{
    public class GetAllMaterialsQuery : IRequest<List<MaterialDto>>;

    public class GetAllMaterialsQueryHandler(IMaterialRepository repository) : IRequestHandler<GetAllMaterialsQuery, List<MaterialDto>>
    {
        public async Task<List<MaterialDto>> Handle(GetAllMaterialsQuery query, CancellationToken cancellationToken)
        {
            List<MaterialEntity> materials = await repository.GetAllAsync();

            return materials.Select(material => new MaterialDto
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
            }).ToList();
        }
    }
}
