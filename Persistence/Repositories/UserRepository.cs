using Domain.Entities;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Persistence.Repositories
{
    public class UserRepository(ApplicationContext context) : IUserRepository
    {
        // CRUD
        public async Task AddAsync(UserEntity user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task<List<UserEntity>> GetAllAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<UserEntity?> GetByIdAsync(Guid id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<UserEntity> UpdateAsync(UserEntity user)
        {
            user.UpdatedAt = DateTime.UtcNow;
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(Guid id)
        {
            context.Users.Remove(new UserEntity { Id = id });
            await context.SaveChangesAsync();
        }

        // Дополнительные методы
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.Users.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null)
        {
            IQueryable<UserEntity> query = context.Users
                .Where(x => x.Email != null && x.Email.ToLower() == email.ToLower());

            if (excludeId.HasValue)
                query = query.Where(x => x.Id != excludeId.Value);

            return !await query.AnyAsync();
        }

        public async Task<bool> IsPhoneUniqueAsync(string phone, Guid? excludeId = null)
        {
            IQueryable<UserEntity> query = context.Users
                .Where(x => x.Phone != null && x.Phone == phone);

            if (excludeId.HasValue)
                query = query.Where(x => x.Id != excludeId.Value);

            return !await query.AnyAsync();
        }

        public async Task<UserEntity?> GetByEmailAsync(string email)
        {
            return await context.Users
                .FirstOrDefaultAsync(x => x.Email != null && x.Email.ToLower() == email.ToLower());
        }
        public async Task<bool> HasAnyUserAsync()
        {
            return await context.Users.AnyAsync();
        }

        // Фильтрация с пагинацией
        public async Task<List<UserEntity>> GetFilteredAsync(
            string? searchTerm = null,
            UserRole? role = null,
            UserStatus? status = null,
            Guid? branchId = null,
            int page = 1,
            int size = 20)
        {
            IQueryable<UserEntity> query = context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string search = searchTerm.Trim().ToLower();
                query = query.Where(x =>
                    x.Name.ToLower().Contains(search) ||
                    (x.Email != null && x.Email.ToLower().Contains(search)) ||
                    (x.Phone != null && x.Phone.Contains(search)) ||
                    (x.Description != null && x.Description.ToLower().Contains(search))
                );
            }

            if (role.HasValue)
                query = query.Where(x => x.Role == role.Value);

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            if (branchId.HasValue)
                query = query.Where(x => x.BranchId == branchId.Value);

            return await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        // Общее количество записей
        public async Task<int> GetTotalCountAsync(
            string? searchTerm = null,
            UserRole? role = null,
            UserStatus? status = null,
            Guid? branchId = null)
        {
            IQueryable<UserEntity> query = context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string search = searchTerm.Trim().ToLower();
                query = query.Where(x =>
                    x.Name.ToLower().Contains(search) ||
                    (x.Email != null && x.Email.ToLower().Contains(search)) ||
                    (x.Phone != null && x.Phone.Contains(search)) ||
                    (x.Description != null && x.Description.ToLower().Contains(search))
                );
            }

            if (role.HasValue)
                query = query.Where(x => x.Role == role.Value);

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            if (branchId.HasValue)
                query = query.Where(x => x.BranchId == branchId.Value);

            return await query.CountAsync();
        }

        // Проверка связей
        public async Task<bool> HasUsersInBranchAsync(Guid branchId)
        {
            return await context.Users.AnyAsync(x => x.BranchId == branchId);
        }
    }
}