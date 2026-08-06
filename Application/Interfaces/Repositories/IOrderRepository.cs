using Domain.Entities;
using Domain.Entities.Supporting;
using Domain.Enums;
using System.Drawing;

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
        Task<List<OrderItemEntity>> GetOrderItemsByOrderIdAsync(Guid orderId);

        // Фильтрация с пагинацией
        Task<List<OrderEntity>> GetFilteredAsync(
            string? searchTerm = null,
            OrderStatus? status = null,
            Guid? clientId = null,
            Guid? branchId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int page = 1,
            int size = 20);

        // Общее количество записей
        Task<int> GetTotalCountAsync(
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