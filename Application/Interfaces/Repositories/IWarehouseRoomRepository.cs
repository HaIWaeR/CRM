using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IWarehouseRoomRepository
    {
        Task AddAsync(WarehouseRoomEntity warehouse);
        Task<List<WarehouseRoomEntity>> GetAllAsync();
        Task<WarehouseRoomEntity?> GetByIdAsync(Guid id);
        Task<WarehouseRoomEntity> UpdateAsync(WarehouseRoomEntity warehouse);
        Task DeleteAsync(Guid id);
    }
}
