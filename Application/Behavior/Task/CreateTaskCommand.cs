using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.Task
{
    public class CreateTaskCommand : IRequest<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? UserId { get; set; }
    }

    public class CreateTaskCommandHandler(ITaskRepository repository) : IRequestHandler<CreateTaskCommand, Guid>
    {
        public async Task<Guid> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            TaskEntity task = new TaskEntity
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                Description = command.Description,
                Status = command.Status,
                ClientId = command.ClientId,
                UserId = command.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(task);
            return task.Id;
        }
    }
}