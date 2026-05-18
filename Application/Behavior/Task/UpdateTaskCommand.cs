using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.Task
{
    public class UpdateTaskCommand : IRequest<TaskDto?>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? UserId { get; set; }
    }

    public class UpdateTaskCommandHandler(ITaskRepository repository) : IRequestHandler<UpdateTaskCommand, TaskDto?>
    {
        public async Task<TaskDto?> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
        {
            TaskEntity? task = await repository.GetByIdAsync(command.Id) ?? throw new Exception($"Задача с ID {command.Id} не найдена");
            task.Title = command.Title;
            task.Description = command.Description;
            task.Status = command.Status;
            task.ClientId = command.ClientId;
            task.UserId = command.UserId;
            task.UpdatedAt = DateTime.UtcNow;
            await repository.UpdateAsync(task);
            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                ClientId = task.ClientId,
                UserId = task.UserId,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }
    }
}