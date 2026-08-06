using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class SupplierRepository(ApplicationContext context) : ISupplierRepository
    {
        // CRUD
        public async Task AddAsync(SupplierEntity supplier)
        {
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

        // Дополнительные методы
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.Suppliers.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> IsInnUniqueAsync(string inn, Guid? excludeId = null)
        {
            IQueryable<SupplierEntity> query = context.Suppliers
                .Where(x => x.Inn != null && x.Inn.ToLower() == inn.ToLower());

            if (excludeId.HasValue)
                query = query.Where(x => x.Id != excludeId.Value);

            return !await query.AnyAsync();
        }

        // Фильтрация
        public async Task<List<SupplierEntity>> GetFilteredAsync(
            string? searchTerm = null,
            SupplierType? supplierType = null,
            SupplierStatus? status = null,
            int? minRating = null,
            int? maxRating = null,
            int page = 1,
            int size = 20)
        {
            IQueryable<SupplierEntity> query = context.Suppliers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string search = searchTerm.Trim().ToLower();
                query = query.Where(x =>
                    x.Name.ToLower().Contains(search) ||
                    (x.Inn != null && x.Inn.ToLower().Contains(search)) ||
                    (x.Address != null && x.Address.ToLower().Contains(search)) ||
                    (x.ContactPerson != null && x.ContactPerson.ToLower().Contains(search)) ||
                    (x.Description != null && x.Description.ToLower().Contains(search))
                );
            }

            if (supplierType.HasValue)
                query = query.Where(x => x.Type == supplierType.Value);

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            if (minRating.HasValue)
                query = query.Where(x => x.Rating >= minRating.Value);

            if (maxRating.HasValue)
                query = query.Where(x => x.Rating <= maxRating.Value);

            return await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        // Общее колличество записей
        public async Task<int> GetTotalCountAsync(
            string? searchTerm = null,
            SupplierType? supplierType = null,
            SupplierStatus? status = null,
            int? minRating = null,
            int? maxRating = null)
        {
            IQueryable<SupplierEntity> query = context.Suppliers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string search = searchTerm.Trim().ToLower();
                query = query.Where(x =>
                    x.Name.ToLower().Contains(search) ||
                    (x.Inn != null && x.Inn.ToLower().Contains(search)) ||
                    (x.Address != null && x.Address.ToLower().Contains(search)) ||
                    (x.ContactPerson != null && x.ContactPerson.ToLower().Contains(search)) ||
                    (x.Description != null && x.Description.ToLower().Contains(search))
                );
            }

            if (supplierType.HasValue)
                query = query.Where(x => x.Type == supplierType.Value);

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            if (minRating.HasValue)
                query = query.Where(x => x.Rating >= minRating.Value);

            if (maxRating.HasValue)
                query = query.Where(x => x.Rating <= maxRating.Value);

            return await query.CountAsync();
        }

        // Проверка связей
        public async Task<bool> HasSupplierMaterialsAsync(Guid supplierId)
        {
            return await context.SupplierMaterials.AnyAsync(x => x.SupplierId == supplierId);
        }
    }
}