using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface IMaterialRepository
    {
        // CRUD 
        Task AddAsync(MaterialEntity material);
        Task<List<MaterialEntity>> GetAllAsync();
        Task<MaterialEntity?> GetByIdAsync(Guid id);
        Task<MaterialEntity> UpdateAsync(MaterialEntity material);
        Task DeleteAsync(Guid id);

        // Дополнительные методы 
        Task<bool> ExistsAsync(Guid id);
        Task<bool> IsArticleUniqueAsync(string article, Guid? excludeId = null);
        Task<MaterialEntity?> GetByArticleAsync(string article);

        // Фильтрация с пагинацией
        Task<List<MaterialEntity>> GetFilteredAsync(
            string? searchTerm = null,
            string? categoryCode = null,
            MaterialStatus? status = null,
            string? article = null,
            int page = 1,
            int size = 20);

        // Общее количество записей
        Task<int> GetTotalCountAsync(
            string? searchTerm = null,
            string? categoryCode = null,
            MaterialStatus? status = null,
            string? article = null);

        // Проверка связей
        Task<bool> HasSuppliersAsync(Guid materialId);
        Task<bool> HasStockItemsAsync(Guid materialId);
    }
}
