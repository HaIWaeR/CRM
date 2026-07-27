using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Entities.Supporting;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class OrderRepository(ApplicationContext context) : IOrderRepository
    {
        // CRUD
        public async Task AddAsync(OrderEntity order)
        {
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
            context.Orders.Update(order);
            await context.SaveChangesAsync();
            return order;
        }

        public async Task DeleteAsync(Guid id)
        {
            context.Orders.Remove(new OrderEntity { Id = id });
            await context.SaveChangesAsync();
        }

        // Дополнительные методы 

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.Orders.AnyAsync(x => x.Id == id);
        }

        public async Task<OrderEntity?> GetByOrderNumberAsync(string orderNumber)
        {
            return await context.Orders.FirstOrDefaultAsync(x => x.OrderNumber == orderNumber);
        }

        public async Task<bool> IsOrderNumberUniqueAsync(string orderNumber, Guid? excludeId = null)
        {
            IQueryable<OrderEntity> query = context.Orders
                .Where(x => x.OrderNumber == orderNumber);

            if (excludeId.HasValue)
                query = query.Where(x => x.Id != excludeId.Value);

            return !await query.AnyAsync();
        }

        public async Task AddOrderItemAsync(OrderItemEntity orderItem)
        {
            await context.OrderItems.AddAsync(orderItem);
            await context.SaveChangesAsync();
        }
        public async Task<List<OrderItemEntity>> GetOrderItemsByOrderIdAsync(Guid orderId)
        {
            return await context.OrderItems
                .Where(x => x.OrderId == orderId)
                .Include(x => x.Product)
                .ToListAsync();
        }

        // Фильтрация 

        public async Task<List<OrderEntity>> GetFilteredAsync(
           string? searchTerm = null,
           OrderStatus? status = null,
           Guid? clientId = null,
           Guid? branchId = null,
           DateTime? fromDate = null,
           DateTime? toDate = null)
        {
            IQueryable<OrderEntity> query = context.Orders.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string search = searchTerm.Trim().ToLower();
                query = query.Where(x =>
                    x.OrderNumber.ToLower().Contains(search) ||
                    x.ServiceName.ToLower().Contains(search) ||
                    (x.Description != null && x.Description.ToLower().Contains(search))
                );
            }

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            if (clientId.HasValue)
                query = query.Where(x => x.ClientId == clientId.Value);

            if (branchId.HasValue)
                query = query.Where(x => x.BranchId == branchId.Value);

            if (fromDate.HasValue)
                query = query.Where(x => x.CreatedAt >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(x => x.CreatedAt <= toDate.Value);

            return await query.ToListAsync();
        }

        // Статистика 
        public async Task<int> GetCountByStatusAsync(OrderStatus status)
        {
            return await context.Orders
                .CountAsync(x => x.Status == status);
        }
    }
}