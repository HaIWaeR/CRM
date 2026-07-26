using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {

        // CRUD
        Task AddAsync(OrderEntity order);
        Task<List<OrderEntity>> GetAllAsync();
        Task<OrderEntity?> GetByIdAsync(Guid id);
        Task<OrderEntity> UpdateAsync(OrderEntity order);
        Task DeleteAsync(Guid id);


        // Дополнительные методы 
        Task<bool> ExistsAsync(Guid id);
        Task<OrderEntity?> GetByOrderNumberAsync(string orderNumber);
        Task<bool> IsOrderNumberUniqueAsync(string orderNumber, Guid? excludeId = null);

        // Фильтрация 
        Task<List<OrderEntity>> GetFilteredAsync(
            string? searchTerm = null,
            OrderStatus? status = null,
            Guid? clientId = null,
            Guid? branchId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null);

        // Статистика 
        Task<int> GetCountByStatusAsync(OrderStatus status);
    }
}
