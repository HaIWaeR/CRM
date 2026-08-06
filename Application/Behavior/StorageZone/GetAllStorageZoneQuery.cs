using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.StorageZone;
using Shared.DTOs.Pagination;

namespace Application.Behavior.StorageZone
{
    public class GetAllStorageZonesQuery : IRequest<PaginatedResult<StorageZoneDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public string? SearchTerm { get; set; }
        public Guid? WarehouseId { get; set; }
        public StorageZoneType? ZoneType { get; set; }
        public StorageZoneStatus? Status { get; set; }
        public bool? IsDefault { get; set; }
    }

    public class GetAllStorageZonesQueryHandler(IStorageZoneRepository repository) : IRequestHandler<GetAllStorageZonesQuery, PaginatedResult<StorageZoneDto>>
    {
        public async Task<PaginatedResult<StorageZoneDto>> Handle(GetAllStorageZonesQuery query, CancellationToken cancellationToken)
        {
            List<StorageZoneEntity> storageZones = await repository.GetFilteredAsync(
                query.SearchTerm,
                query.WarehouseId,
                query.ZoneType,
                query.Status,
                query.IsDefault,
                query.Page,
                query.Size);

            int totalCount = await repository.GetTotalCountAsync(
                query.SearchTerm,
                query.WarehouseId,
                query.ZoneType,
                query.Status,
                query.IsDefault);

            int totalPages = (int)Math.Ceiling(totalCount / (double)query.Size);

            if (query.Page > totalPages && totalPages > 0)
            {
                return new PaginatedResult<StorageZoneDto>
                {
                    Items = new List<StorageZoneDto>(),
                    TotalCount = totalCount,
                    Page = query.Page,
                    Size = query.Size,
                    TotalPages = totalPages
                };
            }

            List<StorageZoneDto> items = storageZones.Adapt<List<StorageZoneDto>>();

            return new PaginatedResult<StorageZoneDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = query.Page,
                Size = query.Size,
                TotalPages = totalPages
            };
        }
    }
}