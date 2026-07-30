using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Task
{
    public class ChangeTaskPriorityCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public TaskPriority Priority { get; set; }
    }

    public class ChangeTaskPriorityCommandHandler(ITaskRepository repository) : IRequestHandler<ChangeTaskPriorityCommand, bool>
    {
        public async Task<bool> Handle(ChangeTaskPriorityCommand command, CancellationToken cancellationToken)
        {
            TaskEntity? task = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Задача с ID {command.Id} не найдена");

            task.Priority = command.Priority;
            task.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(task);
            return true;
        }
    }
}