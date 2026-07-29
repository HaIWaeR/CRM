using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface IWarehouseRoomRepository
    {
        // CRUD
        Task AddAsync(WarehouseRoomEntity warehouse);
        Task<List<WarehouseRoomEntity>> GetAllAsync();
        Task<WarehouseRoomEntity?> GetByIdAsync(Guid id);
        Task<WarehouseRoomEntity> UpdateAsync(WarehouseRoomEntity warehouse);
        Task DeleteAsync(Guid id);

        // Дополнительные методы
        Task<bool> ExistsAsync(Guid id);
            
        // Фильтрация
        Task<List<WarehouseRoomEntity>> GetFilteredAsync(
            string? searchTerm = null,
            Guid? branchId = null,
            WarehouseStatus? status = null);

        // Проверка связей
        Task<bool> HasStorageZonesAsync(Guid warehouseId);
        Task<bool> HasStockItemsAsync(Guid warehouseId);
    }
}