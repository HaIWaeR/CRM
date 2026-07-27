using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.StorageZone
{
    public class ChangeStorageZoneTypeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public StorageZoneType ZoneType { get; set; }
    }

    public class ChangeStorageZoneTypeCommandHandler(IStorageZoneRepository repository) : IRequestHandler<ChangeStorageZoneTypeCommand, bool>
    {
        public async Task<bool> Handle(ChangeStorageZoneTypeCommand command, CancellationToken cancellationToken)
        {
            StorageZoneEntity? storageZone = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Зона хранения с ID {command.Id} не найдена");

            storageZone.ZoneType = command.ZoneType;
            storageZone.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(storageZone);
            return true;
        }
    }
}