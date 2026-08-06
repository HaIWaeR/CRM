using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface IClientRepository
    {
        // CRUD
        Task AddAsync(ClientEntity client);
        Task<List<ClientEntity>> GetAllAsync();
        Task<ClientEntity?> GetByIdAsync(Guid id);
        Task<ClientEntity> UpdateAsync(ClientEntity client);
        Task DeleteAsync(Guid id);

        // Дополнительный метод 
        Task<bool> ExistsAsync(Guid id);
        Task<bool> IsPhoneUniqueAsync(string phone, Guid? excludeId = null);
        Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null);
        Task<bool> IsTelegramUniqueAsync(string telegramId, Guid? excludeId = null);

        // Фильтрация с пагинацией
        Task<List<ClientEntity>> GetFilteredAsync(
            string? searchTerm = null,
            ClientStatus? status = null,
            string? phone = null,
            string? email = null,
            string? telegram = null,
            int page = 1,
            int size = 20);

        // Общее количество записей
        Task<int> GetTotalCountAsync(
            string? searchTerm = null,
            ClientStatus? status = null,
            string? phone = null,
            string? email = null,
            string? telegram = null);

        // Проверка связей
        Task<bool> HasOrdersAsync(Guid clientId);
    }
}
