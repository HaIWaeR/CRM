using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskEntity task);
        Task<List<TaskEntity>> GetAllAsync();
        Task<TaskEntity?> GetByIdAsync(Guid id);
        Task<TaskEntity> UpdateAsync(TaskEntity task);
        Task DeleteAsync(Guid id);
    }
}
