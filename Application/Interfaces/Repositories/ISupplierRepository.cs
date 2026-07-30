using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface ISupplierRepository
    {
        // CRUD
        Task AddAsync(SupplierEntity supplier);
        Task<List<SupplierEntity>> GetAllAsync();
        Task<SupplierEntity?> GetByIdAsync(Guid id);
        Task<SupplierEntity> UpdateAsync(SupplierEntity supplier);
        Task DeleteAsync(Guid id);

        // Дополнительные методы
        Task<bool> ExistsAsync(Guid id);
        Task<bool> IsInnUniqueAsync(string inn, Guid? excludeId = null);

        // Фильтрация
        Task<List<SupplierEntity>> GetFilteredAsync(
            string? searchTerm = null,
            SupplierType? supplierType = null,
            SupplierStatus? supplierStatus = null,
            int? minRating = null,
            int? maxRating = null);

        // Проверка связей
        Task<bool> HasSupplierMaterialsAsync(Guid supplierId);
    }
}