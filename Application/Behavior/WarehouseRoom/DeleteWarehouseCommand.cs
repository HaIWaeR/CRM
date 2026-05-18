using MediatR;
using Application.Interfaces.Repositories;

namespace Application.Behavior.WarehouseRoom
{
    public class DeleteWarehouseRoomCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteWarehouseRoomCommandHandler(IWarehouseRoomRepository repository) : IRequestHandler<DeleteWarehouseRoomCommand, bool>
    {
        public async Task<bool> Handle(DeleteWarehouseRoomCommand command, CancellationToken cancellationToken)
        {
            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}