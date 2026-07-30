using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class TaskRepository(ApplicationContext context) : ITaskRepository
    {
        public async Task AddAsync(TaskEntity task)
        {
            await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();
        }

        public async Task<List<TaskEntity>> GetAllAsync()
        {
            return await context.Tasks
                .Include(x => x.Order)
                .Include(x => x.Client)
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<TaskEntity?> GetByIdAsync(Guid id)
        {
            return await context.Tasks
                .Include(x => x.Order)
                .Include(x => x.Client)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.Tasks.AnyAsync(x => x.Id == id);
        }

        public async Task<List<TaskEntity>> GetFilteredAsync(
            string? searchTerm = null,
            Guid? userId = null,
            Guid? clientId = null,
            Guid? orderId = null,
            TaskPriority? priority = null,
            InstallTaskStatus? status = null,
            DateTime? fromDeadline = null,
            DateTime? toDeadline = null)
        {
            IQueryable<TaskEntity> query = context.Tasks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string search = searchTerm.Trim().ToLower();
                query = query.Where(x =>
                    x.Title.ToLower().Contains(search) ||
                    (x.Description != null && x.Description.ToLower().Contains(search))
                );
            }

            if (userId.HasValue)
                query = query.Where(x => x.UserId == userId.Value);

            if (clientId.HasValue)
                query = query.Where(x => x.ClientId == clientId.Value);

            if (orderId.HasValue)
                query = query.Where(x => x.OrderId == orderId.Value);

            if (priority.HasValue)
                query = query.Where(x => x.Priority == priority.Value);

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            if (fromDeadline.HasValue)
                query = query.Where(x => x.Deadline >= fromDeadline.Value);

            if (toDeadline.HasValue)
                query = query.Where(x => x.Deadline <= toDeadline.Value);

            return await query.ToListAsync();
        }

        public async Task<bool> HasTasksForUserAsync(Guid userId)
        {
            return await context.Tasks.AnyAsync(x => x.UserId == userId);
        }

        public async Task<bool> HasTasksForClientAsync(Guid clientId)
        {
            return await context.Tasks.AnyAsync(x => x.ClientId == clientId);
        }

        public async Task<bool> HasTasksForOrderAsync(Guid orderId)
        {
            return await context.Tasks.AnyAsync(x => x.OrderId == orderId);
        }
    }
}