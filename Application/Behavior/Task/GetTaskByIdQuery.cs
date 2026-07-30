using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.Task;

namespace Application.Behavior.Task
{
    public class GetTaskByIdQuery : IRequest<TaskDto>
    {
        public Guid Id { get; set; }
    }

    public class GetTaskByIdQueryHandler(
        ITaskRepository taskRepository,
        IClientRepository clientRepository,
        IOrderRepository orderRepository,
        IUserRepository userRepository) : IRequestHandler<GetTaskByIdQuery, TaskDto>
    {
        public async Task<TaskDto> Handle(GetTaskByIdQuery query, CancellationToken cancellationToken)
        {
            TaskEntity? task = await taskRepository.GetByIdAsync(query.Id)
                ?? throw new KeyNotFoundException($"Задача с ID {query.Id} не найдена");

            TaskDto result = task.Adapt<TaskDto>();

            if (task.UserId.HasValue)
            {
                UserEntity? user = await userRepository.GetByIdAsync(task.UserId.Value);
                if (user != null)
                {
                    result.User = new UserInfoForTaskDto
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Phone = user.Phone,
                        Email = user.Email
                    };
                }
            }

            if (task.OrderId.HasValue)
            {
                OrderEntity? order = await orderRepository.GetByIdAsync(task.OrderId.Value);
                if (order != null)
                {
                    result.Order = new OrderInfoForTaskDto
                    {
                        Id = order.Id,
                        Number = order.OrderNumber,
                        ServiceName = order.ServiceName
                    };
                }
            }

            if (task.ClientId.HasValue)
            {
                ClientEntity? client = await clientRepository.GetByIdAsync(task.ClientId.Value);
                if (client != null)
                {
                    result.Client = new ClientInfoForTaskDto
                    {
                        Id = client.Id,
                        Name = $"{client.FirstName} {client.LastName}".Trim(),
                        Phone = client.Phone,
                        Email = client.Email,
                        Telegram = client.Telegram
                    };
                }
            }

            return result;
        }
    }
}