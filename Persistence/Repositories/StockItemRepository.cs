using Domain.Entities;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class StockItemRepository(ApplicationContext context) : IStockItemRepository
    {
        // CRUD
        public async Task AddAsync(StockItemEntity stockItem)
        {
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
            context.StockItems.Update(stockItem);
            await context.SaveChangesAsync();
            return stockItem;
        }

        public async Task DeleteAsync(Guid id)
        {
            context.StockItems.Remove(new StockItemEntity { Id = id });
            await context.SaveChangesAsync();
        }

        // Дополнительные методы 
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.StockItems.AnyAsync(x => x.Id == id);
        }

        public async Task<List<StockItemEntity>> GetByProductIdAsync(Guid productId)
        {
            return await context.StockItems
                .Where(x => x.ProductId == productId)
                .Include(x => x.Warehouse)
                .Include(x => x.StorageZone)
                .ToListAsync();
        }

        public async Task<List<StockItemEntity>> GetByMaterialIdAsync(Guid materialId)
        {
            return await context.StockItems
                .Where(x => x.MaterialId == materialId)
                .Include(x => x.Warehouse)
                .Include(x => x.StorageZone)
                .ToListAsync();
        }

        public async Task AddQuantityAsync(Guid id, int quantity)
        {
            StockItemEntity? stockItem = await GetByIdAsync(id);
            if (stockItem == null)
                throw new KeyNotFoundException($"Запись с ID {id} не найдена");

            stockItem.Quantity += quantity;
            stockItem.LastUpdate = DateTime.UtcNow;

            await context.SaveChangesAsync();
        }

        public async Task RemoveQuantityAsync(Guid id, int quantity)
        {
            StockItemEntity? stockItem = await GetByIdAsync(id);
            if (stockItem == null)
                throw new KeyNotFoundException($"Запись с ID {id} не найдена");

            if (stockItem.Quantity < quantity)
                throw new InvalidOperationException($"Недостаточно товара. В наличии: {stockItem.Quantity}, запрошено: {quantity}");

            stockItem.Quantity -= quantity;
            stockItem.LastUpdate = DateTime.UtcNow;

            await context.SaveChangesAsync();
        }

        // Фильтрация
        public async Task<List<StockItemEntity>> GetFilteredAsync(
            Guid? productId = null,
            Guid? materialId = null,
            Guid? warehouseId = null,
            Guid? storageZoneId = null,
            int? minQuantity = null,
            int? maxQuantity = null)
        {
            IQueryable<StockItemEntity> query = context.StockItems
                .Include(x => x.Warehouse)
                .Include(x => x.StorageZone)
                .Include(x => x.Product)
                .Include(x => x.Material);

            if (productId.HasValue)
                query = query.Where(x => x.ProductId == productId.Value);

            if (materialId.HasValue)
                query = query.Where(x => x.MaterialId == materialId.Value);

            if (warehouseId.HasValue)
                query = query.Where(x => x.WarehouseId == warehouseId.Value);

            if (storageZoneId.HasValue)
                query = query.Where(x => x.StorageZoneId == storageZoneId.Value);

            if (minQuantity.HasValue)
                query = query.Where(x => x.Quantity >= minQuantity.Value);

            if (maxQuantity.HasValue)
                query = query.Where(x => x.Quantity <= maxQuantity.Value);

            return await query.ToListAsync();
        }

        // Проверка связей
        public async Task<bool> HasAnyStockItemsForProductAsync(Guid productId)
        {
            return await context.StockItems.AnyAsync(x => x.ProductId == productId);
        }

        public async Task<bool> HasAnyStockItemsForMaterialAsync(Guid materialId)
        {
            return await context.StockItems.AnyAsync(x => x.MaterialId == materialId);
        }

        public async Task<bool> HasAnyStockItemsForWarehouseAsync(Guid warehouseId)
        {
            return await context.StockItems.AnyAsync(x => x.WarehouseId == warehouseId);
        }

        public async Task<bool> HasAnyStockItemsForStorageZoneAsync(Guid storageZoneId)
        {
            return await context.StockItems.AnyAsync(x => x.StorageZoneId == storageZoneId);
        }
    }
}