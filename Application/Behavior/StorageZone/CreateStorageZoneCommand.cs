using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.StorageZone
{
    public class CreateStorageZoneCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public StorageZoneType ZoneType { get; set; }
        public int? MaxCapacity { get; set; }
        public Guid WarehouseId { get; set; }
    }

    public class CreateStorageZoneCommandHandler(IStorageZoneRepository repository) : IRequestHandler<CreateStorageZoneCommand, Guid>
    {
        public async Task<Guid> Handle(CreateStorageZoneCommand command, CancellationToken cancellationToken)
        {
            StorageZoneEntity zone = new StorageZoneEntity
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Code = command.Code,
                ZoneType = command.ZoneType,
                MaxCapacity = command.MaxCapacity,
                WarehouseId = command.WarehouseId,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(zone);
            return zone.Id;
        }
    }
}