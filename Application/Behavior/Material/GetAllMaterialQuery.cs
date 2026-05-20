using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behavior.Material
{
    public class GetAllMaterialsQuery : IRequest<List<MaterialEntity>>;

    public class GetAllMaterialsQueryHandler(IMaterialRepository repository) : IRequestHandler<GetAllMaterialsQuery, List<MaterialEntity>>
    {
        public async Task<List<MaterialEntity>> Handle(GetAllMaterialsQuery query, CancellationToken cancellationToken)
        {
            List<MaterialEntity> materials = await repository.GetAllAsync();

            return materials;
        }
    }
}
