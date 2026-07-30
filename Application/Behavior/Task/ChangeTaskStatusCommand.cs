using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Task
{
    public class ChangeTaskStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public InstallTaskStatus Status { get; set; }
    }

    public class ChangeTaskStatusCommandHandler(ITaskRepository repository) : IRequestHandler<ChangeTaskStatusCommand, bool>
    {
        public async Task<bool> Handle(ChangeTaskStatusCommand command, CancellationToken cancellationToken)
        {
            TaskEntity? task = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Задача с ID {command.Id} не найдена");

            task.Status = command.Status;
            task.UpdatedAt = DateTime.UtcNow;

            if (command.Status == InstallTaskStatus.Completed)
                task.CompletedAt = DateTime.UtcNow;
            else
                task.CompletedAt = null;

            await repository.UpdateAsync(task);
            return true;
        }
    }
}