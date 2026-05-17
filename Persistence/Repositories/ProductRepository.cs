using Domain.Entities;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProductRepository(ApplicationContext context) : IProductRepository
    {
        public async Task AddAsync(ProductEntity product)
        {
            product.Id = Guid.NewGuid();
            product.CreatedAt = DateTime.UtcNow;
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
            product.UpdatedAt = DateTime.UtcNow;
            context.Products.Update(product);
            await context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteAsync(Guid id)
        {
            context.Products.Remove(new ProductEntity { Id = id });
            await context.SaveChangesAsync();
        }
    }
}
