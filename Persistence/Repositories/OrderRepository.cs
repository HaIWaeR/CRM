using Domain.Entities;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class OrderRepository(ApplicationContext context) : IOrderRepository
    {
        public async Task AddAsync(OrderEntity order)
        {
            order.Id = Guid.NewGuid();
            order.CreatedAt = DateTime.UtcNow;
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
        }

        public async Task<List<OrderEntity>> GetAllAsync()
        {
            return await context.Orders.ToListAsync();
        }

        public async Task<OrderEntity?> GetByIdAsync(Guid id)
        {
            return await context.Orders.FindAsync(id);
        }

        public async Task<OrderEntity> UpdateAsync(OrderEntity order)
        {
            order.UpdatedAt = DateTime.UtcNow;
            context.Orders.Update(order);
            await context.SaveChangesAsync();
            return order;
        }

        public async Task DeleteAsync(Guid id)
        {
            context.Orders.Remove(new OrderEntity { Id = id });
            await context.SaveChangesAsync();
        }
    }
}