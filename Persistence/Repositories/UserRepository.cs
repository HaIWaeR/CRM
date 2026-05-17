using Domain.Entities;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class UserRepository(ApplicationContext context) : IUserRepository
    {
        public async Task AddAsync(UserEntity user)
        {
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;
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
    }
}