using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ClientRepository(ApplicationContext context) : IClientRepository
    {
        public async Task AddAsync(ClientEntity client)
        {
            client.Id = Guid.NewGuid();
            client.CreatedAt = DateTime.UtcNow;
            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
        }
        public async Task<List<ClientEntity>> GetAllAsync()
        {
            return await context.Clients.ToListAsync();
        }
        public async Task<ClientEntity?> GetByIdAsync(Guid id)
        {
            return await context.Clients.FindAsync(id);
        }
        public async Task<ClientEntity> UpdateAsync(ClientEntity client)
        {
            client.UpdatedAt = DateTime.UtcNow;
            context.Clients.Update(client);
            await context.SaveChangesAsync();
            return client;
        }
        public async Task DeleteAsync(Guid id)
        {
            context.Clients.Remove(new ClientEntity { Id = id });
            await context.SaveChangesAsync();
        }
    }
}
