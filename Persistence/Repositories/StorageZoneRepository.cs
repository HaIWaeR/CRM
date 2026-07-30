using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class StorageZoneRepository(ApplicationContext context) : IStorageZoneRepository
    {
        // CRUD
        public async Task AddAsync(StorageZoneEntity storageZone)
        {
            await context.Storages.AddAsync(storageZone);
            await context.SaveChangesAsync();
        }

        public async Task<List<StorageZoneEntity>> GetAllAsync()
        {
            return await context.Storages
                .Include(x => x.StockItems)
                .ToListAsync();
        }

        public async Task<StorageZoneEntity?> GetByIdAsync(Guid id)
        {
            return await context.Storages
                .Include(x => x.StockItems)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<StorageZoneEntity> UpdateAsync(StorageZoneEntity storageZone)
        {
            storageZone.UpdatedAt = DateTime.UtcNow;
            context.Storages.Update(storageZone);
            await context.SaveChangesAsync();
            return storageZone;
        }

        public async Task DeleteAsync(Guid id)
        {
            context.Storages.Remove(new StorageZoneEntity { Id = id });
            await context.SaveChangesAsync();
        }

        // Дополнительные методы
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.Storages.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> IsCodeUniqueAsync(string code, Guid? excludeId = null)
        {
            IQueryable<StorageZoneEntity> query = context.Storages
                .Where(x => x.Code != null && x.Code.ToLower() == code.ToLower());

            if (excludeId.HasValue)
                query = query.Where(x => x.Id != excludeId.Value);

            return !await query.AnyAsync();
        }

        // Фильтрация
        public async Task<List<StorageZoneEntity>> GetFilteredAsync(
            string? searchTerm = null,
            Guid? warehouseId = null,
            StorageZoneType? zoneType = null,
            StorageZoneStatus? status = null,
            bool? isDefault = null)
        {
            IQueryable<StorageZoneEntity> query = context.Storages
                .Include(x => x.StockItems)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string search = searchTerm.Trim().ToLower();
                query = query.Where(x =>
                    x.Name.ToLower().Contains(search) ||
                    (x.Code != null && x.Code.ToLower().Contains(search)) ||
                    (x.Description != null && x.Description.ToLower().Contains(search))
                );
            }

            if (warehouseId.HasValue)
                query = query.Where(x => x.WarehouseId == warehouseId.Value);

            if (zoneType.HasValue)
                query = query.Where(x => x.ZoneType == zoneType.Value);

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            if (isDefault.HasValue)
                query = query.Where(x => x.IsDefault == isDefault.Value);

            return await query.ToListAsync();
        }

        // Проверка связей
        public async Task<bool> HasStockItemsAsync(Guid storageZoneId)
        {
            return await context.StockItems.AnyAsync(x => x.StorageZoneId == storageZoneId);
        }

        public async Task<bool> HasAnyStockItemsForWarehouseAsync(Guid warehouseId)
        {
            return await context.StockItems.AnyAsync(x => x.WarehouseId == warehouseId);
        }

        // Обновление статуса зоны (добавлен)
        public async Task UpdateZoneStatusAsync(Guid storageZoneId)
        {
            var zone = await context.Storages
                .Include(x => x.StockItems)
                .FirstOrDefaultAsync(x => x.Id == storageZoneId);

            if (zone == null)
                return;

            if (!zone.MaxCapacity.HasValue || zone.MaxCapacity.Value == 0)
            {
                zone.Status = StorageZoneStatus.Empty;
            }
            else
            {
                int totalQuantity = zone.StockItems?.Sum(x => x.Quantity) ?? 0;

                if (totalQuantity == 0)
                    zone.Status = StorageZoneStatus.Empty;
                else if (totalQuantity >= zone.MaxCapacity.Value)
                    zone.Status = StorageZoneStatus.FullyOccupied;
                else
                    zone.Status = StorageZoneStatus.PartiallyOccupied;
            }

            zone.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
        }
    }
}