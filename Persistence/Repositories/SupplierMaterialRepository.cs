using Domain.Entities;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class SupplierMaterialRepository(ApplicationContext context) : ISupplierMaterialRepository
    {
        public async Task AddAsync(SupplierMaterialEntity supplierMaterial)
        {
            supplierMaterial.Id = Guid.NewGuid();
            supplierMaterial.CreatedAt = DateTime.UtcNow;
            await context.SupplierMaterials.AddAsync(supplierMaterial);
            await context.SaveChangesAsync();
        }

        public async Task<List<SupplierMaterialEntity>> GetAllAsync()
        {
            return await context.SupplierMaterials.ToListAsync();
        }

        public async Task<SupplierMaterialEntity?> GetByIdAsync(Guid id)
        {
            return await context.SupplierMaterials.FindAsync(id);
        }

        public async Task<SupplierMaterialEntity> UpdateAsync(SupplierMaterialEntity supplierMaterial)
        {
            context.SupplierMaterials.Update(supplierMaterial);
            await context.SaveChangesAsync();
            return supplierMaterial;
        }

        public async Task DeleteAsync(Guid id)
        {
            context.SupplierMaterials.Remove(new SupplierMaterialEntity { Id = id });
            await context.SaveChangesAsync();
        }
    }
}