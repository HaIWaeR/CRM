using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Behavior.StorageZone
{
    public class CreateStorageZoneCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public int? MaxCapacity { get; set; }
        public string? Description { get; set; }
        public bool IsDefault { get; set; }
        public Guid WarehouseId { get; set; }
    }

    public class CreateStorageZoneCommandHandler(IStorageZoneRepository repository) : IRequestHandler<CreateStorageZoneCommand, Guid>
    {
        public async Task<Guid> Handle(CreateStorageZoneCommand command, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(command.Code))
            {
                if (!await repository.IsCodeUniqueAsync(command.Code))
                    throw new InvalidOperationException($"Код '{command.Code}' уже существует");
            }

            StorageZoneEntity storageZone = command.Adapt<StorageZoneEntity>();
            storageZone.Id = Guid.NewGuid();
            storageZone.CreatedAt = DateTime.UtcNow;
            storageZone.Status = StorageZoneStatus.Empty;
            storageZone.ZoneType = StorageZoneType.None;

            await repository.AddAsync(storageZone);
            return storageZone.Id;
        }
    }
}