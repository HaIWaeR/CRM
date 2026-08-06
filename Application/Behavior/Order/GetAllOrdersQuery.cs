using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.Order;
using Shared.DTOs.Pagination;

namespace Application.Behavior.Orders
{
    public class GetAllOrdersQuery : IRequest<PaginatedResult<OrderDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public OrderStatus? Status { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? BranchId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class GetAllOrdersQueryHandler(
        IOrderRepository orderRepository,
        IClientRepository clientRepository) : IRequestHandler<GetAllOrdersQuery, PaginatedResult<OrderDto>>
    {
        public async Task<PaginatedResult<OrderDto>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
        {
            List<OrderEntity> orders = await orderRepository.GetFilteredAsync(
                query.SearchTerm,
                query.Status,
                query.ClientId,
                query.BranchId,
                query.FromDate,
                query.ToDate,
                query.Page,
                query.Size);

            int totalCount = await orderRepository.GetTotalCountAsync(
                query.SearchTerm,
                query.Status,
                query.ClientId,
                query.BranchId,
                query.FromDate,
                query.ToDate);

            int totalPages = (int)Math.Ceiling(totalCount / (double)query.Size);

            if (query.Page > totalPages && totalPages > 0)
            {
                return new PaginatedResult<OrderDto>
                {
                    Items = new List<OrderDto>(),
                    TotalCount = totalCount,
                    Page = query.Page,
                    Size = query.Size,
                    TotalPages = totalPages
                };
            }

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

            return new PaginatedResult<OrderDto>
            {
                Items = result,
                TotalCount = totalCount,
                Page = query.Page,
                Size = query.Size,
                TotalPages = totalPages
            };
        }
    }
}