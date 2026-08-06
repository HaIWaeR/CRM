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
            var order = await context.Orders
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (order != null)
            {
                context.OrderItems.RemoveRange(order.OrderItems);
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
            }
        }

        // Дополнительные методы
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.Orders.AnyAsync(x => x.Id == id);
        }

        public async Task<List<OrderItemEntity>> GetOrderItemsByOrderIdAsync(Guid orderId)
        {
            return await context.OrderItems
                .Where(x => x.OrderId == orderId)
                .Include(x => x.Product)
                .ToListAsync();
        }

        // Фильтрация с пагинацией
        public async Task<List<OrderEntity>> GetFilteredAsync(
           string? searchTerm = null,
           OrderStatus? status = null,
           Guid? clientId = null,
           Guid? branchId = null,
           DateTime? fromDate = null,
           DateTime? toDate = null,
           int page = 1,
           int size = 20)
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

            return await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        // Общее колличество записей
        public async Task<int> GetTotalCountAsync(
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

            return await query.CountAsync();
        }


        // Статистика
        public async Task<int> GetCountByStatusAsync(OrderStatus status)
        {
            return await context.Orders
                .CountAsync(x => x.Status == status);
        }
    }
}