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

    public class GetAllOrdersQueryHandler(IOrderRepository repository) : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
    {
        public async Task<List<OrderDto>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
        {
            List<OrderEntity> orders = await repository.GetFilteredAsync(
                query.SearchTerm,
                query.Status,
                query.ClientId,
                query.BranchId,
                query.FromDate,
                query.ToDate);

            List<OrderDto> result = orders.Adapt<List<OrderDto>>();
            return result;
        }
    }
}

