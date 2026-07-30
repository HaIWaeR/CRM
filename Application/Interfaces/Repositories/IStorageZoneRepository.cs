using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface IStorageZoneRepository
    {
        // CRUD
        Task AddAsync(StorageZoneEntity storageZone);
        Task<List<StorageZoneEntity>> GetAllAsync();
        Task<StorageZoneEntity?> GetByIdAsync(Guid id);
        Task<StorageZoneEntity> UpdateAsync(StorageZoneEntity storageZone);
        Task DeleteAsync(Guid id);

        // Дополнительные методы
        Task<bool> ExistsAsync(Guid id);
        Task<bool> IsCodeUniqueAsync(string code, Guid? excludeId = null);

        // Фильтрация
        Task<List<StorageZoneEntity>> GetFilteredAsync(
            string? searchTerm = null,
            Guid? warehouseId = null,
            StorageZoneType? zoneType = null,
            StorageZoneStatus? status = null,
            bool? isDefault = null);

        // Проверка связей
        Task<bool> HasStockItemsAsync(Guid storageZoneId);
        Task<bool> HasAnyStockItemsForWarehouseAsync(Guid warehouseId);

        // Обновление статуса зоны
        Task UpdateZoneStatusAsync(Guid storageZoneId);
    }
}