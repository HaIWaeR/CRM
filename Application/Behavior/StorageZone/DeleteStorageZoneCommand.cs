using MediatR;
using Application.Interfaces.Repositories;

namespace Application.Behavior.StorageZone
{
    public class DeleteStorageZoneCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteStorageZoneCommandHandler(IStorageZoneRepository repository) : IRequestHandler<DeleteStorageZoneCommand, bool>
    {
        public async Task<bool> Handle(DeleteStorageZoneCommand command, CancellationToken cancellationToken)
        {
            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}