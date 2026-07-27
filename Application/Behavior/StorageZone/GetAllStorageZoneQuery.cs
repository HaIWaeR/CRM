using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.StorageZone;

namespace Application.Behavior.StorageZone
{
    public class GetAllStorageZonesQuery : IRequest<List<StorageZoneDto>>
    {
        public string? SearchTerm { get; set; }
        public Guid? WarehouseId { get; set; }
        public StorageZoneType? ZoneType { get; set; }
        public StorageZoneStatus? Status { get; set; }
        public bool? IsDefault { get; set; }
    }

    public class GetAllStorageZonesQueryHandler(IStorageZoneRepository repository) : IRequestHandler<GetAllStorageZonesQuery, List<StorageZoneDto>>
    {
        public async Task<List<StorageZoneDto>> Handle(GetAllStorageZonesQuery query, CancellationToken cancellationToken)
        {
            List<StorageZoneEntity> storageZones = await repository.GetFilteredAsync(
                query.SearchTerm,
                query.WarehouseId,
                query.ZoneType,
                query.Status,
                query.IsDefault);

            List<StorageZoneDto> result = storageZones.Adapt<List<StorageZoneDto>>();
            return result;
        }
    }
}