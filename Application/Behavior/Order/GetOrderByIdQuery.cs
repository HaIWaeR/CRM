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

    public class GetOrderByIdQueryHandler(IOrderRepository repository) : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        public async Task<OrderDto> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            OrderEntity? order = await repository.GetByIdAsync(query.Id)
                ?? throw new KeyNotFoundException($"Заказ с ID {query.Id} не найден");

            OrderDto result = order.Adapt<OrderDto>();
            return result;
        }
    }

}