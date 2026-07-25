using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.Material;

namespace Application.Behavior.Material
{
    public class GetMaterialByIdQuery : IRequest<MaterialDto>
    {
        public Guid Id { get; set; }
    }

    public class GetMaterialByIdQueryHandler(IMaterialRepository repository) : IRequestHandler<GetMaterialByIdQuery, MaterialDto>
    {
        public async Task<MaterialDto> Handle(GetMaterialByIdQuery query, CancellationToken cancellationToken)
        {
            MaterialEntity? material = await repository.GetByIdAsync(query.Id)
                ?? throw new KeyNotFoundException($"Материал с ID {query.Id} не найден");

            MaterialDto result = material.Adapt<MaterialDto>();
            return result;
        }
    }
}
