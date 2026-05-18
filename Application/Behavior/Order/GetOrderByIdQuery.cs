using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.Orders
{
    public class GetOrderByIdQuery : IRequest<OrderDto?>
    {
        public Guid Id { get; set; }
    }

    public class GetOrderByIdQueryHandler(IOrderRepository repository) : IRequestHandler<GetOrderByIdQuery, OrderDto?>
    {
        public async Task<OrderDto?> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            OrderEntity? order = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Заказ с ID {query.Id} не найден");

            return new OrderDto
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
            };
        }
    }
}