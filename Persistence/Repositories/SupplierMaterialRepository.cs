using Domain.Entities;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class SupplierMaterialRepository(ApplicationContext context) : ISupplierMaterialRepository
    {
        // CRUD
        public async Task AddAsync(SupplierMaterialEntity supplierMaterial)
        {
            await context.SupplierMaterials.AddAsync(supplierMaterial);
            await context.SaveChangesAsync();
        }

        public async Task<List<SupplierMaterialEntity>> GetAllAsync()
        {
            return await context.SupplierMaterials
                .Include(x => x.Supplier)
                .Include(x => x.Material)
                .ToListAsync();
        }

        public async Task<SupplierMaterialEntity?> GetByIdAsync(Guid id)
        {
            return await context.SupplierMaterials
                .Include(x => x.Supplier)
                .Include(x => x.Material)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SupplierMaterialEntity> UpdateAsync(SupplierMaterialEntity supplierMaterial)
        {
            supplierMaterial.UpdatedAt = DateTime.UtcNow;
            context.SupplierMaterials.Update(supplierMaterial);
            await context.SaveChangesAsync();
            return supplierMaterial;
        }

        public async Task DeleteAsync(Guid id)
        {
            context.SupplierMaterials.Remove(new SupplierMaterialEntity { Id = id });
            await context.SaveChangesAsync();
        }

        // Дополнительные методы
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.SupplierMaterials.AnyAsync(x => x.Id == id);
        }

        // Фильтрация
        public async Task<List<SupplierMaterialEntity>> GetFilteredAsync(
            Guid? supplierId = null,
            Guid? materialId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? maxDeliveryDays = null)
        {
            IQueryable<SupplierMaterialEntity> query = context.SupplierMaterials
                .Include(x => x.Supplier)
                .Include(x => x.Material);

            if (supplierId.HasValue)
                query = query.Where(x => x.SupplierId == supplierId.Value);

            if (materialId.HasValue)
                query = query.Where(x => x.MaterialId == materialId.Value);

            if (minPrice.HasValue)
                query = query.Where(x => x.PriceUnit >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(x => x.PriceUnit <= maxPrice.Value);

            if (maxDeliveryDays.HasValue)
                query = query.Where(x => x.DeliveryDays <= maxDeliveryDays.Value);

            return await query.ToListAsync();
        }

        // Проверка связей
        public async Task<bool> HasSupplierMaterialsAsync(Guid supplierId)
        {
            return await context.SupplierMaterials.AnyAsync(x => x.SupplierId == supplierId);
        }

        public async Task<bool> HasSupplierMaterialsForMaterialAsync(Guid materialId)
        {
            return await context.SupplierMaterials.AnyAsync(x => x.MaterialId == materialId);
        }
    }
}