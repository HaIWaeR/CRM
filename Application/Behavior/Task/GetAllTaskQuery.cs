using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.Task
{
    public class GetAllTasksQuery : IRequest<List<TaskEntity>>;

    public class GetAllTasksQueryHandler(ITaskRepository repository) : IRequestHandler<GetAllTasksQuery, List<TaskEntity>>
    {
        public async Task<List<TaskEntity>> Handle(GetAllTasksQuery query, CancellationToken cancellationToken)
        {
            List<TaskEntity> tasks = await repository.GetAllAsync();

            return tasks;
        }
    }
}