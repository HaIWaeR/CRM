using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface IBranchRepository
    {
        // CRUD
        Task AddAsync(BranchEntity branch);
        Task<List<BranchEntity>> GetAllAsync();
        Task<BranchEntity?> GetByIdAsync(Guid id);
        Task<BranchEntity> UpdateAsync(BranchEntity branch);
        Task DeleteAsync(Guid id);

        // Дополнительные методы
        Task<bool> ChangeBranchStatusAsync(Guid id, BranchStatus status);
        Task<bool> ExistsAsync(Guid id);

        // Фильтрация с пагинацией
        Task<List<BranchEntity>> GetFilteredAsync(
            string? searchTerm = null, 
            BranchStatus? status = null,
            int page = 1,
            int size = 20);

        // Общее колличество записей
        Task<int> GetTotalCountAsync(
            string? searchTerm = null,
            BranchStatus? status = null);


        // Проверка связей
        Task<bool> HasUsersAsync(Guid branchId);
        Task<bool> HasOrdersAsync(Guid branchId);
        Task<bool> HasWarehousesAsync(Guid branchId);
    }
}