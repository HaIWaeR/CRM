using Domain.Entities;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class MaterialRepository(ApplicationContext context) : IMaterialRepository
    {
        public async Task AddAsync(MaterialEntity material)
        {
            material.Id = Guid.NewGuid();
            material.CreatedAt = DateTime.UtcNow;
            await context.Materials.AddAsync(material);
            await context.SaveChangesAsync();
        }

        public async Task<List<MaterialEntity>> GetAllAsync()
        {
            return await context.Materials.ToListAsync();
        }

        public async Task<MaterialEntity?> GetByIdAsync(Guid id)
        {
            return await context.Materials.FindAsync(id);
        }

        public async Task<MaterialEntity> UpdateAsync(MaterialEntity material)
        {
            material.UpdatedAt = DateTime.UtcNow;
            context.Materials.Update(material);
            await context.SaveChangesAsync();
            return material;
        }

        public async Task DeleteAsync(Guid id)
        {
            context.Materials.Remove(new MaterialEntity { Id = id });
            await context.SaveChangesAsync();
        }
    }
}
