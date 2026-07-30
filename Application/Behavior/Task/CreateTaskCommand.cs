using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Behavior.Task
{
    public class CreateTaskCommand : IRequest<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime? Deadline { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ClientId { get; set; }
    }

    public class CreateTaskCommandHandler(ITaskRepository repository) : IRequestHandler<CreateTaskCommand, Guid>
    {
        public async Task<Guid> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            TaskEntity task = command.Adapt<TaskEntity>();
            task.Id = Guid.NewGuid();
            task.Priority = TaskPriority.Medium;
            task.Status = InstallTaskStatus.New;
            task.CreatedAt = DateTime.UtcNow;

            await repository.AddAsync(task);
            return task.Id;
        }
    }
}