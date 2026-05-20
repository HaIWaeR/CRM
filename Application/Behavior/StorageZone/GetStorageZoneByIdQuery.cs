using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.StorageZone
{
    public class GetStorageZoneByIdQuery : IRequest<StorageZoneEntity?>
    {
        public Guid Id { get; set; }
    }

    public class GetStorageZoneByIdQueryHandler(IStorageZoneRepository repository) : IRequestHandler<GetStorageZoneByIdQuery, StorageZoneEntity?>
    {
        public async Task<StorageZoneEntity?> Handle(GetStorageZoneByIdQuery query, CancellationToken cancellationToken)
        {
            StorageZoneEntity? zone = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Зона хранения с ID {query.Id} не найдена");

            return zone;
        }
    }
}