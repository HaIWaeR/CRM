using MediatR;
using Application.Interfaces.Repositories;

namespace Application.Behavior.Task
{
    public class DeleteTaskCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteTaskCommandHandler(ITaskRepository repository) : IRequestHandler<DeleteTaskCommand, bool>
    {
        public async Task<bool> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
        {
            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}