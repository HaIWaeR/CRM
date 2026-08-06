using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class WarehouseRoomRepository(ApplicationContext context) : IWarehouseRoomRepository
    {
        // CRUD
        public async Task AddAsync(WarehouseRoomEntity warehouse)
        {
            await context.Warehouses.AddAsync(warehouse);
            await context.SaveChangesAsync();
        }

        public async Task<List<WarehouseRoomEntity>> GetAllAsync()
        {
            return await context.Warehouses.ToListAsync();
        }

        public async Task<WarehouseRoomEntity?> GetByIdAsync(Guid id)
        {
            return await context.Warehouses.FindAsync(id);
        }

        public async Task<WarehouseRoomEntity> UpdateAsync(WarehouseRoomEntity warehouse)
        {
            warehouse.UpdatedAt = DateTime.UtcNow;
            context.Warehouses.Update(warehouse);
            await context.SaveChangesAsync();
            return warehouse;
        }

        public async Task DeleteAsync(Guid id)
        {
            context.Warehouses.Remove(new WarehouseRoomEntity { Id = id });
            await context.SaveChangesAsync();
        }

        // Дополнительные методы
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.Warehouses.AnyAsync(x => x.Id == id);
        }

        // Фильтрация с пагинацией
        public async Task<List<WarehouseRoomEntity>> GetFilteredAsync(
            string? searchTerm = null,
            Guid? branchId = null,
            WarehouseStatus? status = null,
            int page = 1,
            int size = 20)
        {
            IQueryable<WarehouseRoomEntity> query = context.Warehouses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string search = searchTerm.Trim().ToLower();
                query = query.Where(x =>
                    x.Name.ToLower().Contains(search) ||
                    x.Address.ToLower().Contains(search) ||
                    (x.Description != null && x.Description.ToLower().Contains(search))
                );
            }

            if (branchId.HasValue)
                query = query.Where(x => x.BranchId == branchId.Value);

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            return await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        // Общее количество записей
        public async Task<int> GetTotalCountAsync(
            string? searchTerm = null,
            Guid? branchId = null,
            WarehouseStatus? status = null)
        {
            IQueryable<WarehouseRoomEntity> query = context.Warehouses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string search = searchTerm.Trim().ToLower();
                query = query.Where(x =>
                    x.Name.ToLower().Contains(search) ||
                    x.Address.ToLower().Contains(search) ||
                    (x.Description != null && x.Description.ToLower().Contains(search))
                );
            }

            if (branchId.HasValue)
                query = query.Where(x => x.BranchId == branchId.Value);

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            return await query.CountAsync();
        }

        // Проверка связей
        public async Task<bool> HasStorageZonesAsync(Guid warehouseId)
        {
            return await context.Storages.AnyAsync(x => x.WarehouseId == warehouseId);
        }

        public async Task<bool> HasStockItemsAsync(Guid warehouseId)
        {
            return await context.StockItems.AnyAsync(x => x.WarehouseId == warehouseId);
        }
    }
}