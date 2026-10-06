using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    /// <summary>
    /// Реализация <see cref="IClientRepository"/> на EF Core для PostgreSQL.
    /// </summary>
    /// <param name="context">Контекст БД; передаётся через DI с временем жизни Scoped.</param>
    public class ClientRepository(ApplicationContext context) : IClientRepository
    {
        // CRUD

        /// <inheritdoc/>
        public async Task AddAsync(ClientEntity client)
        {
            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
        }
        /// <inheritdoc/>
        public async Task<List<ClientEntity>> GetAllAsync()
        {
            return await context.Clients.ToListAsync();
        }
        /// <inheritdoc/>
        public async Task<ClientEntity?> GetByIdAsync(Guid id)
        {
            return await context.Clients.FindAsync(id);
        }
        /// <inheritdoc/>
        public async Task<ClientEntity> UpdateAsync(ClientEntity client)
        {
            context.Clients.Update(client);
            await context.SaveChangesAsync();
            return client;
        }
        /// <inheritdoc/>
        public async Task DeleteAsync(Guid id)
        {
            context.Clients.Remove(new ClientEntity { Id = id });
            await context.SaveChangesAsync();
        }
        
        // Дополнительные методы
        /// <inheritdoc/>
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.Clients.AnyAsync(x => x.Id == id);
        }

        /// <inheritdoc/>
        public async Task<bool> IsPhoneUniqueAsync(string phone, Guid? excludeId = null)
        {
            IQueryable<ClientEntity> query = context.Clients
                .Where(x => x.Phone != null && x.Phone == phone);

            if (excludeId.HasValue)
                query = query.Where(x => x.Id != excludeId.Value);

            return !await query.AnyAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null)
        {
            IQueryable<ClientEntity> query = context.Clients
                .Where(x => x.Email != null && x.Email.ToLower() == email.ToLower());

            if (excludeId.HasValue)
                query = query.Where(x => x.Id != excludeId.Value);

            return !await query.AnyAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> IsTelegramUniqueAsync(string telegram, Guid? excludeId = null)
        {
            IQueryable<ClientEntity> query = context.Clients
                .Where(x => x.Telegram != null && x.Telegram == telegram);

            if (excludeId.HasValue)
                query = query.Where(x => x.Id != excludeId.Value);

            return !await query.AnyAsync();
        }

        // Фильтрация с пагинацией
        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
        /// <inheritdoc/>
        public async Task<bool> HasOrdersAsync(Guid clientId)
        {
            return await context.Orders.AnyAsync(x => x.ClientId == clientId);
        }
    }
}
