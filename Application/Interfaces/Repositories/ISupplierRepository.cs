using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ISupplierRepository
    {
        Task AddAsync(SupplierEntity supplier);
        Task<List<SupplierEntity>> GetAllAsync();
        Task<SupplierEntity?> GetByIdAsync(Guid id);
        Task<SupplierEntity> UpdateAsync(SupplierEntity supplier);
        Task DeleteAsync(Guid id);
    }
}
