using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface ISupplierMaterialRepository
    {
        // CRUD
        Task AddAsync(SupplierMaterialEntity supplierMaterial);
        Task<List<SupplierMaterialEntity>> GetAllAsync();
        Task<SupplierMaterialEntity?> GetByIdAsync(Guid id);
        Task<SupplierMaterialEntity> UpdateAsync(SupplierMaterialEntity supplierMaterial);
        Task DeleteAsync(Guid id);

        // Дополнительные методы
        Task<bool> ExistsAsync(Guid id);

        // Фильтрация с пагинацией
        Task<List<SupplierMaterialEntity>> GetFilteredAsync(
            Guid? supplierId = null,
            Guid? materialId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? maxDeliveryDays = null,
            int page = 1,
            int size = 20);

        // Общее количество записей
        Task<int> GetTotalCountAsync(
            Guid? supplierId = null,
            Guid? materialId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? maxDeliveryDays = null);

        // Проверка связей
        Task<bool> HasSupplierMaterialsAsync(Guid supplierId);
        Task<bool> HasSupplierMaterialsForMaterialAsync(Guid materialId);
    }
}