using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task AddAsync(ClientEntity client);
        Task<List<ClientEntity>> GetAllAsync();
        Task<ClientEntity?> GetByIdAsync(Guid id);
        Task<ClientEntity> UpdateAsync(ClientEntity client);
        Task DeleteAsync(Guid id);

        Task<ClientEntity?> GetByNameAsync(string name);
        Task<ClientEntity?> GetByEmailAsync(string email);
        Task<ClientEntity?> GetByPhoneAsync(string phone);
        Task<ClientEntity?> GetByTelegramAsync(string telegram);

        Task<bool> HasOrdersAsync(Guid clientId);
    }
}
