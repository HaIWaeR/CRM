using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behavior.Material
{
    public class GetMaterialByIdQuery : IRequest<MaterialEntity?>
    {
        public Guid Id { get; set; }
    }

    public class GetMaterialByIdQueryHandler(IMaterialRepository repository) : IRequestHandler<GetMaterialByIdQuery, MaterialEntity?>
    {
        public async Task<MaterialEntity?> Handle(GetMaterialByIdQuery query, CancellationToken cancellationToken)
        {
            MaterialEntity? material = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Материал с ID {query.Id} не найден");

            return material;
        }
    }
}
