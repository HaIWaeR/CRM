using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.Task
{
    public class GetTaskByIdQuery : IRequest<TaskDto?>
    {
        public Guid Id { get; set; }
    }

    public class GetTaskByIdQueryHandler(ITaskRepository repository) : IRequestHandler<GetTaskByIdQuery, TaskDto?>
    {
        public async Task<TaskDto?> Handle(GetTaskByIdQuery query, CancellationToken cancellationToken)
        {
            TaskEntity? task = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Задача с ID {query.Id} не найдена");
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