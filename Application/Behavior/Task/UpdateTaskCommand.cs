using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.Task
{
    public class UpdateTaskCommand : IRequest<TaskEntity?>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? UserId { get; set; }
    }

    public class UpdateTaskCommandHandler(ITaskRepository repository) : IRequestHandler<UpdateTaskCommand, TaskEntity?>
    {
        public async Task<TaskEntity?> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
        {
            TaskEntity? task = await repository.GetByIdAsync(command.Id) ?? throw new Exception($"Задача с ID {command.Id} не найдена");
            task.Title = command.Title;
            task.Description = command.Description;
            task.Status = command.Status;
            task.ClientId = command.ClientId;
            task.UserId = command.UserId;
            task.UpdatedAt = DateTime.UtcNow;
            await repository.UpdateAsync(task);

            return task;
        }
    }
}