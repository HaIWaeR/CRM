using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.Material;

namespace Application.Behavior.Material
{
    public class GetAllMaterialsQuery : IRequest<List<MaterialDto>>
    {
        public string? SearchTerm { get; set; }
        public string? CategoryCode { get; set; }
        public bool? IsActive { get; set; }
        public string? Article { get; set; }
    }

    public class GetAllMaterialsQueryHandler(IMaterialRepository repository) : IRequestHandler<GetAllMaterialsQuery, List<MaterialDto>>
    {
        public async Task<List<MaterialDto>> Handle(GetAllMaterialsQuery query, CancellationToken cancellationToken)
        {
            List<MaterialEntity> materials = await repository.GetFilteredAsync(
                query.SearchTerm,
                query.CategoryCode,
                query.IsActive,
                query.Article);

            List<MaterialDto> result = materials.Adapt<List<MaterialDto>>();
            return result;
        }
    }
}
