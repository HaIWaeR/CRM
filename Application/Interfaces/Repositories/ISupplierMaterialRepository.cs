using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ISupplierMaterialRepository
    {
        Task AddAsync(SupplierMaterialEntity supplierMaterial);
        Task<List<SupplierMaterialEntity>> GetAllAsync();
        Task<SupplierMaterialEntity?> GetByIdAsync(Guid id);
        Task<SupplierMaterialEntity> UpdateAsync(SupplierMaterialEntity supplierMaterial);
        Task DeleteAsync(Guid id);
    }
}
