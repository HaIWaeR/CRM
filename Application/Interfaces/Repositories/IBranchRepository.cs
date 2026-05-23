using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface IBranchRepository
    {
        Task AddAsync(BranchEntity branch);
        Task<List<BranchEntity>> GetAllAsync();
        Task<BranchEntity?> GetByIdAsync(Guid id);
        Task<BranchEntity>UpdateAsync(BranchEntity branch);
        Task DeleteAsync (Guid id);
        Task<bool> ChangeBranchStatusAsync(Guid id, BranchStatus status);

        Task<BranchEntity?> GetByNameAsync(string name);

        Task<bool> HasUsersAsync(Guid branchId);
        Task<bool> HasOrdersAsync(Guid branchId);
        Task<bool> HasWarehousesAsync(Guid branchId);
    }
}
