using Domain.Entities;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class MaterialRepository(ApplicationContext context) : IMaterialRepository
    {
        // CRUD
        public async Task AddAsync(MaterialEntity material)
        {
            await context.Materials.AddAsync(material);
            await context.SaveChangesAsync();
        }

        public async Task<List<MaterialEntity>> GetAllAsync()
        {
            return await context.Materials.ToListAsync();
        }

        public async Task<MaterialEntity?> GetByIdAsync(Guid id)
        {
            return await context.Materials.FindAsync(id);
        }

        public async Task<MaterialEntity> UpdateAsync(MaterialEntity material)
        {
            context.Materials.Update(material);
            await context.SaveChangesAsync();
            return material;
        }

        public async Task DeleteAsync(Guid id)
        {
            context.Materials.Remove(new MaterialEntity { Id = id });
            await context.SaveChangesAsync();
        }

        public async Task<MaterialEntity?> GetByArticleAsync(string article) =>
            await context.Materials.FirstOrDefaultAsync(m => m.Article == article);

        // Дополнительные методы
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.Materials.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> IsArticleUniqueAsync(string article, Guid? excludeId = null)
        {
            IQueryable<MaterialEntity> query = context.Materials
                .Where(x => x.Article.ToLower() == article.ToLower());

            if (excludeId.HasValue)
                query = query.Where(x => x.Id != excludeId.Value);

            return !await query.AnyAsync();
        }

        // Фильтрация 
        public async Task<List<MaterialEntity>> GetFilteredAsync(
            string? searchTerm = null,
            string? categoryCode = null,
            bool? isActive = null,
            string? article = null)
        {
            IQueryable<MaterialEntity> query = context.Materials.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string search = searchTerm.Trim().ToLower();
                query = query.Where(x =>
                    x.Name.ToLower().Contains(search) ||
                    x.Article.ToLower().Contains(search) ||
                    (x.Description != null && x.Description.ToLower().Contains(search))
                );
            }

            if (!string.IsNullOrWhiteSpace(categoryCode))
            {
                query = query.Where(x => x.CategoryCode == categoryCode);
            }

            if (isActive.HasValue)
            {
                query = query.Where(x => x.IsActive == isActive.Value);
            }

            if (!string.IsNullOrWhiteSpace(article))
            {
                query = query.Where(x => x.Article.ToLower() == article.ToLower());
            }

            return await query.ToListAsync();
        }

        // Проверка связей 

        public async Task<bool> HasSuppliersAsync(Guid materialId)
        {
            return await context.SupplierMaterials
                .AnyAsync(x => x.MaterialId == materialId);
        }
        public async Task<bool> HasStockItemsAsync(Guid materialId)
        {
            return await context.StockItems
                .AnyAsync(x => x.MaterialId == materialId);
        }

    }
}
