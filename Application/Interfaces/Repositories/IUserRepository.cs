using Domain.Entities;
using Domain.Enums;
using System.ComponentModel;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        // CRUD
        Task AddAsync(UserEntity user);
        Task<List<UserEntity>> GetAllAsync();
        Task<UserEntity?> GetByIdAsync(Guid id);
        Task<UserEntity> UpdateAsync(UserEntity user);
        Task DeleteAsync(Guid id);

        // Дополнительные методы
        Task<bool> ExistsAsync(Guid id);
        Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null);
        Task<bool> IsPhoneUniqueAsync(string phone, Guid? excludeId = null);
        Task<UserEntity?> GetByEmailAsync(string email);
        Task<bool> HasAnyUserAsync();

        // Фильтрация с пагинацией
        Task<List<UserEntity>> GetFilteredAsync(
            string? searchTerm = null,
            UserRole? role = null,
            UserStatus? status = null,
            Guid? branchId = null,
            int page = 1,
            int size = 20);

        // Общее количество записей
        Task<int> GetTotalCountAsync(
            string? searchTerm = null,
            UserRole? role = null,
            UserStatus? status = null,
            Guid? branchId = null);

        // Проверка связей
        Task<bool> HasUsersInBranchAsync(Guid branchId);
    }
}