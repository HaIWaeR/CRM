using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(OrderEntity order);
        Task<List<OrderEntity>> GetAllAsync();
        Task<OrderEntity?> GetByIdAsync(Guid id);
        Task<OrderEntity> UpdateAsync(OrderEntity order);
        Task DeleteAsync(Guid id);
    }
}
