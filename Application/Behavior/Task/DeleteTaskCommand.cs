using Application.Interfaces.Repositories;
using MediatR;

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
            if (!await repository.ExistsAsync(command.Id))
                throw new KeyNotFoundException($"Задача с ID {command.Id} не найдена");

            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}