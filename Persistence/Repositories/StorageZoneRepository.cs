using Domain.Entities;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class StorageZoneRepository(ApplicationContext context) : IStorageZoneRepository
    {
        public async Task AddAsync(StorageZone storageZone)
        {
            storageZone.Id = Guid.NewGuid();
            storageZone.CreatedAt = DateTime.UtcNow;
            await context.Storages.AddAsync(storageZone);
            await context.SaveChangesAsync();
        }

        public async Task<List<StorageZone>> GetAllAsync()
        {
            return await context.Storages.ToListAsync();
        }

        public async Task<StorageZone?> GetByIdAsync(Guid id)
        {
            return await context.Storages.FindAsync(id);
        }

        public async Task<StorageZone> UpdateAsync(StorageZone storageZone)
        {
            storageZone.UpdatedAt = DateTime.UtcNow;
            context.Storages.Update(storageZone);
            await context.SaveChangesAsync();
            return storageZone;
        }

        public async Task DeleteAsync(Guid id)
        {
            context.Storages.Remove(new StorageZone { Id = id });
            await context.SaveChangesAsync();
        }
    }
}