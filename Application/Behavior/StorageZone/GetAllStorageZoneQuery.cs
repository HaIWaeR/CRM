using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.StorageZone
{
    public class GetAllStorageZonesQuery : IRequest<List<StorageZoneDto>>;

    public class GetAllStorageZonesQueryHandler(IStorageZoneRepository repository) : IRequestHandler<GetAllStorageZonesQuery, List<StorageZoneDto>>
    {
        public async Task<List<StorageZoneDto>> Handle(GetAllStorageZonesQuery query, CancellationToken cancellationToken)
        {
            List<StorageZoneEntity> zones = await repository.GetAllAsync();
            return zones.Select(zone => new StorageZoneDto
            {
                Id = zone.Id,
                Name = zone.Name,
                Code = zone.Code,
                ZoneType = zone.ZoneType,
                MaxCapacity = zone.MaxCapacity,
                WarehouseId = zone.WarehouseId,
                CreatedAt = zone.CreatedAt,
                UpdatedAt = zone.UpdatedAt
            }).ToList();
        }
    }
}