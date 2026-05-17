using Domain.Entities;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class WarehouseRepository(ApplicationContext context) : IWarehouseRepository
    {
        public async Task AddAsync(WarehouseEntity warehouse)
        {
            warehouse.Id = Guid.NewGuid();
            warehouse.CreatedAt = DateTime.UtcNow;
            await context.Warehouses.AddAsync(warehouse);
            await context.SaveChangesAsync();
        }

        public async Task<List<WarehouseEntity>> GetAllAsync()
        {
            return await context.Warehouses.ToListAsync();
        }

        public async Task<WarehouseEntity?> GetByIdAsync(Guid id)
        {
            return await context.Warehouses.FindAsync(id);
        }

        public async Task<WarehouseEntity> UpdateAsync(WarehouseEntity warehouse)
        {
            warehouse.UpdatedAt = DateTime.UtcNow;
            context.Warehouses.Update(warehouse);
            await context.SaveChangesAsync();
            return warehouse;
        }

        public async Task DeleteAsync(Guid id)
        {
            context.Warehouses.Remove(new WarehouseEntity { Id = id });
            await context.SaveChangesAsync();
        }
    }
}