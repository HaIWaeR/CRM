using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IMaterialRepository
    {
        Task AddAsync(MaterialEntity material);
        Task<List<MaterialEntity>> GetAllAsync();
        Task<MaterialEntity?> GetByIdAsync(Guid id);
        Task<MaterialEntity> UpdateAsync(MaterialEntity material);
        Task DeleteAsync(Guid id);

        Task<MaterialEntity?> GetByArticleAsync(string article);
        Task<string?> GetLastArticleByCategoryAsync(string prefix);
    }
}
