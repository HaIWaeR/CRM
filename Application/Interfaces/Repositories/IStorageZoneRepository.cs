using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IStorageZoneRepository
    {
        Task AddAsync(StorageZoneEntity storageZone);
        Task<List<StorageZoneEntity>> GetAllAsync();
        Task<StorageZoneEntity?> GetByIdAsync(Guid id);
        Task<StorageZoneEntity> UpdateAsync(StorageZoneEntity storageZone);
        Task DeleteAsync(Guid id);
    }
}
