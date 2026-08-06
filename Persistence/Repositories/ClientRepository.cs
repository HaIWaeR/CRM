using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ClientRepository(ApplicationContext context) : IClientRepository
    {
        // CRUD
        public async Task AddAsync(ClientEntity client)
        {
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
            context.Clients.Update(client);
            await context.SaveChangesAsync();
            return client;
        }
        public async Task DeleteAsync(Guid id)
        {
            context.Clients.Remove(new ClientEntity { Id = id });
            await context.SaveChangesAsync();
        }
        
        // Дополнительные методы
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.Clients.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> IsPhoneUniqueAsync(string phone, Guid? excludeId = null)
        {
            IQueryable<ClientEntity> query = context.Clients
                .Where(x => x.Phone != null && x.Phone == phone);

            if (excludeId.HasValue)
                query = query.Where(x => x.Id != excludeId.Value);

            return !await query.AnyAsync();
        }

        public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null)
        {
            IQueryable<ClientEntity> query = context.Clients
                .Where(x => x.Email != null && x.Email.ToLower() == email.ToLower());

            if (excludeId.HasValue)
                query = query.Where(x => x.Id != excludeId.Value);

            return !await query.AnyAsync();
        }

        public async Task<bool> IsTelegramUniqueAsync(string telegram, Guid? excludeId = null)
        {
            IQueryable<ClientEntity> query = context.Clients
                .Where(x => x.Telegram != null && x.Telegram == telegram);

            if (excludeId.HasValue)
                query = query.Where(x => x.Id != excludeId.Value);

            return !await query.AnyAsync();
        }

        // Фильтрация с пагинацией
        public async Task<List<ClientEntity>> GetFilteredAsync(
            string? searchTerm = null,
            ClientStatus? status = null,
            string? phone = null,
            string? email = null,
            string? telegram = null,
            int page = 1,
            int size = 10)
        {
            IQueryable<ClientEntity> query = context.Clients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string search = searchTerm.Trim().ToLower();
                query = query.Where(x =>
                    x.FirstName.ToLower().Contains(search) ||
                    (x.LastName != null && x.LastName.ToLower().Contains(search)) ||
                    (x.MiddleName != null && x.MiddleName.ToLower().Contains(search)) ||
                    (x.Phone != null && x.Phone.Contains(search)) ||
                    (x.Email != null && x.Email.ToLower().Contains(search))
                );
            }

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            if (!string.IsNullOrWhiteSpace(phone))
                query = query.Where(x => x.Phone != null && x.Phone == phone);

            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(x => x.Email != null && x.Email.ToLower() == email.ToLower());

            if (!string.IsNullOrWhiteSpace(telegram))
                query = query.Where(x => x.Telegram != null && x.Telegram.Contains(telegram));

            return await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        // Общее количество записей
        public async Task<int> GetTotalCountAsync(
            string? searchTerm = null,
            ClientStatus? status = null,
            string? phone = null,
            string? email = null,
            string? telegram = null)
        {
            IQueryable<ClientEntity> query = context.Clients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string search = searchTerm.Trim().ToLower();
                query = query.Where(x =>
                    x.FirstName.ToLower().Contains(search) ||
                    (x.LastName != null && x.LastName.ToLower().Contains(search)) ||
                    (x.MiddleName != null && x.MiddleName.ToLower().Contains(search)) ||
                    (x.Phone != null && x.Phone.Contains(search)) ||
                    (x.Email != null && x.Email.ToLower().Contains(search))
                );
            }

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            if (!string.IsNullOrWhiteSpace(phone))
                query = query.Where(x => x.Phone != null && x.Phone == phone);

            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(x => x.Email != null && x.Email.ToLower() == email.ToLower());

            if (!string.IsNullOrWhiteSpace(telegram))
                query = query.Where(x => x.Telegram != null && x.Telegram.Contains(telegram));

            return await query.CountAsync();
        }

        // Проверка связей
        public async Task<bool> HasOrdersAsync(Guid clientId)
        {
            return await context.Orders.AnyAsync(x => x.ClientId == clientId);
        }
    }
}
