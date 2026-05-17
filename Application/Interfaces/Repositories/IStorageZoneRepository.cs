using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IStorageZoneRepository
    {
        Task AddAsync(StorageZone storageZone);
        Task<List<StorageZone>> GetAllAsync();
        Task<StorageZone?> GetByIdAsync(Guid id);
        Task<StorageZone> UpdateAsync(StorageZone storageZone);
        Task DeleteAsync(Guid id);
    }
}
