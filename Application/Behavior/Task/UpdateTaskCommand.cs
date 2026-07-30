using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.Task;

namespace Application.Behavior.Task
{
    public class UpdateTaskCommand : IRequest<TaskDto>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime? Deadline { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ClientId { get; set; }
    }

    public class UpdateTaskCommandHandler(ITaskRepository repository) : IRequestHandler<UpdateTaskCommand, TaskDto>
    {
        public async Task<TaskDto> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
        {
            TaskEntity? existing = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Задача с ID {command.Id} не найдена");

            existing.Title = command.Title;
            existing.Description = command.Description;
            existing.AssignedAt = command.AssignedAt;
            existing.Deadline = command.Deadline;
            existing.OrderId = command.OrderId;
            existing.ClientId = command.ClientId;
            existing.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(existing);

            TaskDto result = existing.Adapt<TaskDto>();
            return result;
        }
    }
}