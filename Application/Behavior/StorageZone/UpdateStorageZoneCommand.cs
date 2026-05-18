using Application.DTO;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.StorageZone
{
    public class UpdateStorageZoneCommand : IRequest<StorageZoneDto?>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public StorageZoneType ZoneType { get; set; }
        public int? MaxCapacity { get; set; }
        public Guid WarehouseId { get; set; }
    }

    public class UpdateStorageZoneCommandHandler(IStorageZoneRepository repository) : IRequestHandler<UpdateStorageZoneCommand, StorageZoneDto?>
    {
        public async Task<StorageZoneDto?> Handle(UpdateStorageZoneCommand command, CancellationToken cancellationToken)
        {
            StorageZoneEntity? zone = await repository.GetByIdAsync(command.Id) ?? throw new Exception($"Зона хранения с ID {command.Id} не найдена");
            zone.Name = command.Name;
            zone.Code = command.Code;
            zone.ZoneType = command.ZoneType;
            zone.MaxCapacity = command.MaxCapacity;
            zone.WarehouseId = command.WarehouseId;
            zone.UpdatedAt = DateTime.UtcNow;
            await repository.UpdateAsync(zone);
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