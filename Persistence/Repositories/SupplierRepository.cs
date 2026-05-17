using Domain.Entities;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class SupplierRepository(ApplicationContext context) : ISupplierRepository
    {
        public async Task AddAsync(SupplierEntity supplier)
        {
            supplier.Id = Guid.NewGuid();
            supplier.CreatedAt = DateTime.UtcNow;
            await context.Suppliers.AddAsync(supplier);
            await context.SaveChangesAsync();
        }

        public async Task<List<SupplierEntity>> GetAllAsync()
        {
            return await context.Suppliers.ToListAsync();
        }

        public async Task<SupplierEntity?> GetByIdAsync(Guid id)
        {
            return await context.Suppliers.FindAsync(id);
        }

        public async Task<SupplierEntity> UpdateAsync(SupplierEntity supplier)
        {
            supplier.UpdatedAt = DateTime.UtcNow;
            context.Suppliers.Update(supplier);
            await context.SaveChangesAsync();
            return supplier;
        }

        public async Task DeleteAsync(Guid id)
        {
            context.Suppliers.Remove(new SupplierEntity { Id = id });
            await context.SaveChangesAsync();
        }
    }
}