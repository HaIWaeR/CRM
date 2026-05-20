using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.StorageZone
{
    public class GetAllStorageZonesQuery : IRequest<List<StorageZoneEntity>>;

    public class GetAllStorageZonesQueryHandler(IStorageZoneRepository repository) : IRequestHandler<GetAllStorageZonesQuery, List<StorageZoneEntity>>
    {
        public async Task<List<StorageZoneEntity>> Handle(GetAllStorageZonesQuery query, CancellationToken cancellationToken)
        {
            List<StorageZoneEntity> zones = await repository.GetAllAsync();
            return zones;
        }
    }
}