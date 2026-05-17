using Domain.Entities;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class StockItemRepository(ApplicationContext context) : IStockItemRepository
    {
        public async Task AddAsync(StockItemEntity stockItem)
        {
            stockItem.Id = Guid.NewGuid();
            stockItem.LastUpdate = DateTime.UtcNow;
            await context.StockItems.AddAsync(stockItem);
            await context.SaveChangesAsync();
        }

        public async Task<List<StockItemEntity>> GetAllAsync()
        {
            return await context.StockItems.ToListAsync();
        }

        public async Task<StockItemEntity?> GetByIdAsync(Guid id)
        {
            return await context.StockItems.FindAsync(id);
        }

        public async Task<StockItemEntity> UpdateAsync(StockItemEntity stockItem)
        {
            stockItem.LastUpdate = DateTime.UtcNow;
            context.StockItems.Update(stockItem);
            await context.SaveChangesAsync();
            return stockItem;
        }

        public async Task DeleteAsync(Guid id)
        {
            context.StockItems.Remove(new StockItemEntity { Id = id });
            await context.SaveChangesAsync();
        }
    }
}