using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.Order;

namespace Application.Behavior.Orders
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public Guid Id { get; set; }
    }

    public class GetOrderByIdQueryHandler(IOrderRepository orderRepository,IClientRepository clientRepository) 
        : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        public async Task<OrderDto> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            OrderEntity? order = await orderRepository.GetByIdAsync(query.Id)
                ?? throw new KeyNotFoundException($"Заказ с ID {query.Id} не найден");

            order.OrderItems = await orderRepository.GetOrderItemsByOrderIdAsync(order.Id);

            OrderDto result = order.Adapt<OrderDto>();

            if (order.ClientId.HasValue)
            {
                ClientEntity? client = await clientRepository.GetByIdAsync(order.ClientId.Value);
                if (client != null)
                {
                    result.Client = client.Adapt<ClientInfoDto>();
                }
            }

            return result;
        }
    }

}