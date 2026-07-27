using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.StorageZone;

namespace Application.Behavior.StorageZone
{
    public class GetStorageZoneByIdQuery : IRequest<StorageZoneDto>
    {
        public Guid Id { get; set; }
    }

    public class GetStorageZoneByIdQueryHandler(IStorageZoneRepository repository) : IRequestHandler<GetStorageZoneByIdQuery, StorageZoneDto>
    {
        public async Task<StorageZoneDto> Handle(GetStorageZoneByIdQuery query, CancellationToken cancellationToken)
        {
            StorageZoneEntity? storageZone = await repository.GetByIdAsync(query.Id)
                ?? throw new KeyNotFoundException($"Зона хранения с ID {query.Id} не найдена");

            StorageZoneDto result = storageZone.Adapt<StorageZoneDto>();
            return result;
        }
    }
}