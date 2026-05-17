using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IMaterialRepository
    {
        Task AddAsync(MaterialEntity material);
        Task<List<MaterialEntity>> GetAllAsync();
        Task<MaterialEntity?> GetByIdAsync(Guid Id);
        Task<MaterialEntity> UpdateAsync(MaterialEntity material);
        Task DeleteAsync(Guid Id);
    }
}
