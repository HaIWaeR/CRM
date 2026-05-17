using Domain.Entities;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class TaskRepository(ApplicationContext context) : ITaskRepository
    {
        public async Task AddAsync(TaskEntity task)
        {
            task.Id = Guid.NewGuid();
            task.CreatedAt = DateTime.UtcNow;
            await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();
        }

        public async Task<List<TaskEntity>> GetAllAsync()
        {
            return await context.Tasks.ToListAsync();
        }

        public async Task<TaskEntity?> GetByIdAsync(Guid id)
        {
            return await context.Tasks.FindAsync(id);
        }

        public async Task<TaskEntity> UpdateAsync(TaskEntity task)
        {
            task.UpdatedAt = DateTime.UtcNow;
            context.Tasks.Update(task);
            await context.SaveChangesAsync();
            return task;
        }

        public async Task DeleteAsync(Guid id)
        {
            context.Tasks.Remove(new TaskEntity { Id = id });
            await context.SaveChangesAsync();
        }
    }
}