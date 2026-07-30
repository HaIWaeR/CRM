using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        // CRUD
        Task AddAsync(TaskEntity task);
        Task<List<TaskEntity>> GetAllAsync();
        Task<TaskEntity?> GetByIdAsync(Guid id);
        Task<TaskEntity> UpdateAsync(TaskEntity task);
        Task DeleteAsync(Guid id);

        // Дополнительные методы
        Task<bool> ExistsAsync(Guid id);

        // Фильтрация
        Task<List<TaskEntity>> GetFilteredAsync(
            string? searchTerm = null,
            Guid? userId = null,
            Guid? clientId = null,
            Guid? orderId = null,
            TaskPriority? priority = null,
            InstallTaskStatus? status = null,
            DateTime? fromDeadline = null,
            DateTime? toDeadline = null);

        // Проверка связей
        Task<bool> HasTasksForUserAsync(Guid userId);
        Task<bool> HasTasksForClientAsync(Guid clientId);
        Task<bool> HasTasksForOrderAsync(Guid orderId);
    }
}