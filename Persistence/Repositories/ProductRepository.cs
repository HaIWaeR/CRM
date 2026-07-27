using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProductRepository(ApplicationContext context) : IProductRepository
    {
        // CRUD
        public async Task AddAsync(ProductEntity product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
        }

        public async Task<List<ProductEntity>> GetAllAsync()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<ProductEntity?> GetByIdAsync(Guid id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task<ProductEntity> UpdateAsync(ProductEntity product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteAsync(Guid id)
        {
            context.Products.Remove(new ProductEntity { Id = id });
            await context.SaveChangesAsync();
        }

        // Дополнительные методы 
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.Products.AnyAsync(x => x.Id == id);
        }

        public async Task<ProductEntity?> GetByArticleAsync(string article)
        {
            return await context.Products
                .FirstOrDefaultAsync(x => x.Article != null && x.Article == article);
        }

        // Фильтрация
        public async Task<List<ProductEntity>> GetFilteredAsync(
            string? searchTerm = null,
            string? category = null,
            bool? isActive = null,
            bool? isService = null,
            decimal? minPrice = null,
            decimal? maxPrice = null)
        {
            IQueryable<ProductEntity> query = context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string search = searchTerm.Trim().ToLower();
                query = query.Where(x =>
                    x.Name.ToLower().Contains(search) ||
                    (x.Article != null && x.Article.ToLower().Contains(search)) ||
                    (x.Description != null && x.Description.ToLower().Contains(search))
                );
            }

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(x => x.Category == category);

            if (isActive.HasValue)
                query = query.Where(x => x.IsActive == isActive.Value);

            if (isService.HasValue)
                query = query.Where(x => x.IsService == isService.Value);

            if (minPrice.HasValue)
                query = query.Where(x => x.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(x => x.Price <= maxPrice.Value);

            return await query.ToListAsync();
        }

        // Проверка связе
        public async Task<bool> HasStockItemsAsync(Guid productId)
        {
            return await context.StockItems.AnyAsync(x => x.ProductId == productId);
        }

    }
}
