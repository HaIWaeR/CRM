using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task AddAsync(ClientEntity client);
        Task<List<ClientEntity>> GetAllAsync();
        Task<ClientEntity?> GetByIdAsync(Guid id);
        Task<ClientEntity> UpdateAsync(ClientEntity client);
        Task DeleteAsync(Guid id);
    }
}
