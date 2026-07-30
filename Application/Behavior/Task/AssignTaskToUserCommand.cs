using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behavior.Task
{
    public class AssignTaskToUserCommand : IRequest<bool>
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
    }

    public class AssignTaskToUserCommandHandler(ITaskRepository repository) : IRequestHandler<AssignTaskToUserCommand, bool>
    {
        public async Task<bool> Handle(AssignTaskToUserCommand command, CancellationToken cancellationToken)
        {
            TaskEntity? task = await repository.GetByIdAsync(command.TaskId)
                ?? throw new KeyNotFoundException($"Задача с ID {command.TaskId} не найдена");

            task.UserId = command.UserId;
            task.AssignedAt = DateTime.UtcNow;
            task.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(task);
            return true;
        }
    }
}