using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.StorageZone
{
    public class GetStorageZoneByIdQuery : IRequest<StorageZoneDto?>
    {
        public Guid Id { get; set; }
    }

    public class GetStorageZoneByIdQueryHandler(IStorageZoneRepository repository) : IRequestHandler<GetStorageZoneByIdQuery, StorageZoneDto?>
    {
        public async Task<StorageZoneDto?> Handle(GetStorageZoneByIdQuery query, CancellationToken cancellationToken)
        {
            StorageZoneEntity? zone = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Зона хранения с ID {query.Id} не найдена");
            return new StorageZoneDto
            {
                Id = zone.Id,
                Name = zone.Name,
                Code = zone.Code,
                ZoneType = zone.ZoneType,
                MaxCapacity = zone.MaxCapacity,
                WarehouseId = zone.WarehouseId,
                CreatedAt = zone.CreatedAt,
                UpdatedAt = zone.UpdatedAt
            };
        }
    }
}