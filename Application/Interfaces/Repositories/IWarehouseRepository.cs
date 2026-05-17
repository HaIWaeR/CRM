using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IWarehouseRepository
    {
        Task AddAsync(WarehouseEntity warehouse);
        Task<List<WarehouseEntity>> GetAllAsync();
        Task<WarehouseEntity?> GetByIdAsync(Guid id);
        Task<WarehouseEntity> UpdateAsync(WarehouseEntity warehouse);
        Task DeleteAsync(Guid id);
    }
}
