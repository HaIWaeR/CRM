using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(ProductEntity product);
        Task<List<ProductEntity>> GetAllAsync();
        Task<ProductEntity?> GetByIdAsync(Guid id);
        Task<ProductEntity> UpdateAsync(ProductEntity product);
        Task DeleteAsync(Guid id);
    }
}
