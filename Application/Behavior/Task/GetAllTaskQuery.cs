using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.Task
{
    public class GetAllTasksQuery : IRequest<List<TaskDto>>;

    public class GetAllTasksQueryHandler(ITaskRepository repository) : IRequestHandler<GetAllTasksQuery, List<TaskDto>>
    {
        public async Task<List<TaskDto>> Handle(GetAllTasksQuery query, CancellationToken cancellationToken)
        {
            List<TaskEntity> tasks = await repository.GetAllAsync();
            return tasks.Select(task => new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                ClientId = task.ClientId,
                UserId = task.UserId,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            }).ToList();
        }
    }
}