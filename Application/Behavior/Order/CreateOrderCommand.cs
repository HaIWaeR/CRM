using Application.Helpers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Entities.Supporting;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.Order;

namespace Application.Behavior.Orders
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public string ServiceName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Address { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? BranchId { get; set; }
        public List<OrderItemRequestDto> OrderItems { get; set; } = [];
    }

    public class CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository) : IRequestHandler<CreateOrderCommand, Guid>
    {
        public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            OrderEntity order = command.Adapt<OrderEntity>();
            order.Id = Guid.NewGuid();
            order.OrderNumber = OrderNumberGenerator.Generate(command.ServiceName);
            order.Status = OrderStatus.New;
            order.CreatedAt = DateTime.UtcNow;
            order.Price = 0;
            order.OrderItems = new List<OrderItemEntity>();

            decimal totalPrice = 0;

            foreach (OrderItemRequestDto item in command.OrderItems)
            {
                ProductEntity? product = await productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new KeyNotFoundException($"Товар с ID {item.ProductId} не найден");

                OrderItemEntity orderItem = new OrderItemEntity
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    PriceAtOrder = product.Price,
                    CreatedAt = DateTime.UtcNow,
                };

                order.OrderItems.Add(orderItem);
                totalPrice += orderItem.PriceAtOrder * orderItem.Quantity;
            }

            order.Price = totalPrice;

            await orderRepository.AddAsync(order);

            return order.Id;
        }
    }
}