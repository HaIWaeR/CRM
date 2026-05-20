using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behavior.Orders
{
    public class GetAllOrdersQuery : IRequest<List<OrderEntity>>;

    public class GetAllOrdersQueryHandler(IOrderRepository repository) : IRequestHandler<GetAllOrdersQuery, List<OrderEntity>>
    {
        public async Task<List<OrderEntity>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
        {
            List<OrderEntity> orders = await repository.GetAllAsync();

            return orders;
        }
    }
}

