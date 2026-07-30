using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        // CRUD
        Task AddAsync(ProductEntity product);
        Task<List<ProductEntity>> GetAllAsync();
        Task<ProductEntity?> GetByIdAsync(Guid id);
        Task<ProductEntity> UpdateAsync(ProductEntity product);
        Task DeleteAsync(Guid id);

        // Дополнительные методы 
        Task<bool> ExistsAsync(Guid id);
        Task<ProductEntity?> GetByArticleAsync(string article);

        // Фильтрация
        Task<List<ProductEntity>> GetFilteredAsync(
            string? searchTerm = null,
            string? category = null,
            ProductStatus? status = null,
            bool? isService = null,
            decimal? minPrice = null,
            decimal? maxPrice = null);

        // Проверка связей
        Task<bool> HasStockItemsAsync(Guid productId);
    }
}
