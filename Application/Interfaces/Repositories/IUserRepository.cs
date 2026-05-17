using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity user);
        Task<List<UserEntity>> GetAllAsync();
        Task<UserEntity?> GetByIdAsync(Guid id);
        Task<UserEntity> UpdateAsync(UserEntity user);
        Task DeleteAsync(Guid id);
    }
}
