using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IBranchRepository
    {
        Task AddAsync(BranchEntity branch);
        Task<List<BranchEntity>> GetAllAsync();
        Task<BranchEntity?> GetByIdAsync(Guid Id);
        Task<BranchEntity>UpdateAsync(BranchEntity branch);
        Task DeleteAsync (Guid Id);
    }
}
