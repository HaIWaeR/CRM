using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.StorageZone
{
    public class UpdateStorageZoneCommand : IRequest<StorageZoneEntity?>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public StorageZoneType ZoneType { get; set; }
        public int? MaxCapacity { get; set; }
        public Guid WarehouseId { get; set; }
    }

    public class UpdateStorageZoneCommandHandler(IStorageZoneRepository repository) : IRequestHandler<UpdateStorageZoneCommand, StorageZoneEntity?>
    {
        public async Task<StorageZoneEntity?> Handle(UpdateStorageZoneCommand command, CancellationToken cancellationToken)
        {
            StorageZoneEntity? zone = await repository.GetByIdAsync(command.Id) ?? throw new Exception($"Зона хранения с ID {command.Id} не найдена");
            zone.Name = command.Name;
            zone.Code = command.Code;
            zone.ZoneType = command.ZoneType;
            zone.MaxCapacity = command.MaxCapacity;
            zone.WarehouseId = command.WarehouseId;
            zone.UpdatedAt = DateTime.UtcNow;
            await repository.UpdateAsync(zone);

            return zone;
        }
    }
}