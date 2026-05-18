using Application.DTO;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behavior.Orders
{
    public class GetAllOrdersQuery : IRequest<List<OrderDto>>;

    public class GetAllOrdersQueryHandler(IOrderRepository repository) : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
    {
        public async Task<List<OrderDto>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
        {
            List<OrderEntity> orders = await repository.GetAllAsync();

            return orders.Select(order => new OrderDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                ServiceName = order.ServiceName,
                Price = order.Price,
                Status = order.Status,
                Description = order.Description,
                Address = order.Address,
                ClientId = order.ClientId,
                BranchId = order.BranchId
            }).ToList();
        }
    }
}

