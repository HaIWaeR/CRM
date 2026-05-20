using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.Task
{
    public class GetTaskByIdQuery : IRequest<TaskEntity?>
    {
        public Guid Id { get; set; }
    }

    public class GetTaskByIdQueryHandler(ITaskRepository repository) : IRequestHandler<GetTaskByIdQuery, TaskEntity?>
    {
        public async Task<TaskEntity?> Handle(GetTaskByIdQuery query, CancellationToken cancellationToken)
        {
            TaskEntity? task = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Задача с ID {query.Id} не найдена");

            return task;
        }
    }
}