using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface IBranchRepository
    {
        // CRUT
        Task AddAsync(BranchEntity branch);
        Task<List<BranchEntity>> GetAllAsync();
        Task<BranchEntity?> GetByIdAsync(Guid id);
        Task<BranchEntity>UpdateAsync(BranchEntity branch);
        Task DeleteAsync (Guid id);

        // Доп бизнес логика
        Task<bool> ChangeBranchStatusAsync(Guid id, BranchStatus status);

        // Фильтрация 
        Task<List<BranchEntity>> GetFilteredAsync(string? searchTerm = null, BranchStatus? status = null);

        // Связи с фелиалом
        Task<bool> HasUsersAsync(Guid branchId);
        Task<bool> HasOrdersAsync(Guid branchId);
        Task<bool> HasWarehousesAsync(Guid branchId);
    }
}
