using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.Orders
{
    public class GetOrderByIdQuery : IRequest<OrderEntity?>
    {
        public Guid Id { get; set; }
    }

    public class GetOrderByIdQueryHandler(IOrderRepository repository) : IRequestHandler<GetOrderByIdQuery, OrderEntity?>
    {
        public async Task<OrderEntity?> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            OrderEntity? order = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Заказ с ID {query.Id} не найден");

            return order;
        }
    }
}