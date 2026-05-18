using Domain.Entities;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class WarehouseRoomRepository(ApplicationContext context) : IWarehouseRoomRepository
    {
        public async Task AddAsync(WarehouseRoomEntity warehouse)
        {
            warehouse.Id = Guid.NewGuid();
            warehouse.CreatedAt = DateTime.UtcNow;
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
    }
}