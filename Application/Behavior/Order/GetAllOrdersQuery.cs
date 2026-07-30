using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.Order;

namespace Application.Behavior.Orders
{
    public class GetAllOrdersQuery : IRequest<List<OrderDto>>
    {
        public string? SearchTerm { get; set; }
        public OrderStatus? Status { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? BranchId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class GetAllOrdersQueryHandler(IOrderRepository orderRepository, IClientRepository clientRepository) 
        : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
    {
        public async Task<List<OrderDto>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
        {
            List<OrderEntity> orders = await orderRepository.GetFilteredAsync(
                query.SearchTerm,
                query.Status,
                query.ClientId,
                query.BranchId,
                query.FromDate,
                query.ToDate);

            List<OrderDto> result = new List<OrderDto>();

            foreach (OrderEntity order in orders)
            {
                order.OrderItems = await orderRepository.GetOrderItemsByOrderIdAsync(order.Id);

                OrderDto orderDto = order.Adapt<OrderDto>();

                if (order.ClientId.HasValue)
                {
                    ClientEntity? client = await clientRepository.GetByIdAsync(order.ClientId.Value);
                    if (client != null)
                    {
                        orderDto.Client = client.Adapt<ClientInfoDto>();
                    }
                }

                result.Add(orderDto);
            }

            return result;
        }
    }
}

