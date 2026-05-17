    using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IStockItemRepository
    {
        Task AddAsync(StockItemEntity stockItem);
        Task<List<StockItemEntity>> GetAllAsync();
        Task<StockItemEntity?> GetByIdAsync(Guid id);
        Task<StockItemEntity> UpdateAsync(StockItemEntity stockItem);
        Task DeleteAsync(Guid id);
    }
}
