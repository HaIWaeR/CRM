using Domain.Enums;

namespace Shared.DTOs.Client
{
    /// <summary>
    /// Данные клиента, которые API возвращает при чтении, обновлении и в списках.
    /// </summary>
    /// <remarks>
    /// Не содержит заказы клиента, чтобы не загружать связанные данные в каждом ответе.
    /// </remarks>
    public class ClientDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }

        public ClientStatus Status { get; set; }
        /// <inheritdoc cref="ClientEntity.Phone"/>
        public string? Phone { get; set; }
        /// <inheritdoc cref="ClientEntity.Email"/>
        public string? Email { get; set; }
        /// <inheritdoc cref="ClientEntity.Telegram"/>
        public string? Telegram { get; set; }
        public long? TelegramId { get; set; }
        public DateTime? LastActivityAt { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}