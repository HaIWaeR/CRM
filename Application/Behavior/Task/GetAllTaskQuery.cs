using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.Task;

namespace Application.Behavior.Task
{
    public class GetAllTasksQuery : IRequest<List<TaskDto>>
    {
        public string? SearchTerm { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? OrderId { get; set; }
        public TaskPriority? Priority { get; set; }
        public InstallTaskStatus? Status { get; set; }
        public DateTime? FromDeadline { get; set; }
        public DateTime? ToDeadline { get; set; }
    }

    public class GetAllTasksQueryHandler(
            ITaskRepository taskRepository,
            IClientRepository clientRepository,
            IOrderRepository orderRepository,
            IUserRepository userRepository)
        : IRequestHandler<GetAllTasksQuery, List<TaskDto>>
    {
        public async Task<List<TaskDto>> Handle(GetAllTasksQuery query, CancellationToken cancellationToken)
        {
            List<TaskEntity> tasks = await taskRepository.GetFilteredAsync(
                query.SearchTerm,
                query.UserId,
                query.ClientId,
                query.OrderId,
                query.Priority,
                query.Status,
                query.FromDeadline,
                query.ToDeadline);

            List<TaskDto> result = new List<TaskDto>();

            foreach (TaskEntity task in tasks)
            {
                TaskDto taskDto = task.Adapt<TaskDto>();

                if (task.ClientId.HasValue)
                {
                    ClientEntity? client = await clientRepository.GetByIdAsync(task.ClientId.Value);
                    if (client != null)
                    {
                        taskDto.Client = client.Adapt<ClientInfoForTaskDto>();
                    }
                }

                if (task.OrderId.HasValue)
                {
                    OrderEntity? order = await orderRepository.GetByIdAsync(task.OrderId.Value);
                    if (order != null)
                    {
                        taskDto.Order = order.Adapt<OrderInfoForTaskDto>();
                    }
                }

                if (task.UserId.HasValue)
                {
                    UserEntity? user = await userRepository.GetByIdAsync(task.UserId.Value);
                    if (user != null)
                    {
                        taskDto.User = user.Adapt<UserInfoForTaskDto>();
                    }
                }

                result.Add(taskDto);
            }

            return result;
        }
    }
}