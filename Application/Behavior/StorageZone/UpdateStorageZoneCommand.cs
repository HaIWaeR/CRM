using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.StorageZone;
using System.Text.Json.Serialization;

namespace Application.Behavior.StorageZone
{
    public class UpdateStorageZoneCommand : IRequest<StorageZoneDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public int? MaxCapacity { get; set; }
        public string? Description { get; set; }
        public bool IsDefault { get; set; }
        public Guid WarehouseId { get; set; }
    }

    public class UpdateStorageZoneCommandHandler(IStorageZoneRepository repository) : IRequestHandler<UpdateStorageZoneCommand, StorageZoneDto>
    {
        public async Task<StorageZoneDto> Handle(UpdateStorageZoneCommand command, CancellationToken cancellationToken)
        {
            StorageZoneEntity? existing = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Зона хранения с ID {command.Id} не найдена");

            if (!string.IsNullOrWhiteSpace(command.Code))
            {
                bool isUnique = await repository.IsCodeUniqueAsync(command.Code, command.Id);
                if (!isUnique)
                    throw new InvalidOperationException($"Код '{command.Code}' уже существует");
            }

            command.Adapt(existing);
            existing.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(existing);

            StorageZoneDto result = existing.Adapt<StorageZoneDto>();
            return result;
        }
    }
}