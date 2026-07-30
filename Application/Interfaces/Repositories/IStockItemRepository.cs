using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IStockItemRepository
    {
        // CRUD
        Task AddAsync(StockItemEntity stockItem);
        Task<List<StockItemEntity>> GetAllAsync();
        Task<StockItemEntity?> GetByIdAsync(Guid id);
        Task<StockItemEntity> UpdateAsync(StockItemEntity stockItem);
        Task DeleteAsync(Guid id);

        // Дополнительные методы
        Task<bool> ExistsAsync(Guid id);
        Task<List<StockItemEntity>> GetByProductIdAsync(Guid productId);
        Task<List<StockItemEntity>> GetByMaterialIdAsync(Guid materialId);
        Task AddQuantityAsync(Guid id, int quantity);
        Task RemoveQuantityAsync(Guid id, int quantity);

        // Фильтрация
        Task<List<StockItemEntity>> GetFilteredAsync(
            Guid? productId = null,
            Guid? materialId = null,
            Guid? warehouseId = null,
            Guid? storageZoneId = null,
            int? minQuantity = null,
            int? maxQuantity = null);

        // Проверка связей
        Task<bool> HasAnyStockItemsForProductAsync(Guid productId);
        Task<bool> HasAnyStockItemsForMaterialAsync(Guid materialId);
        Task<bool> HasAnyStockItemsForWarehouseAsync(Guid warehouseId);
        Task<bool> HasAnyStockItemsForStorageZoneAsync(Guid storageZoneId);
    }
}