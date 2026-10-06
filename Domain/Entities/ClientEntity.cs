using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Клиент компании: контактные данные, статус и история заказов.
    /// Хранится в таблице <c>Clients</c>.
    /// </summary>
    /// <remarks>
    /// У клиента должен быть указан хотя бы один контакт: телефон, Email или Telegram.
    /// Каждый из контактов уникален среди всех клиентов.
    /// </remarks>
    public class ClientEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// Телефон клиента в формате <c>+7 000 000 00 00</c>.
        /// Приводится к этому формату при сохранении. Уникален, до 20 символов.
        /// </summary>
        public string? Phone { get; set; }
        /// <summary>
        /// Email клиента. Уникален без учёта регистра, до 100 символов.
        /// </summary>
        public string? Email { get; set ; }
        /// <summary>
        /// Имя пользователя в Telegram, начинается с <c>@</c>. Уникально, до 50 символов.
        /// </summary>
        public string? Telegram { get; set; }
        public long? TelegramId {  get; set; }

        public ClientStatus Status { get; set; } 
        public DateTime? LastActivityAt { get; set; }

        public string? Address { get; set; }
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<OrderEntity> Orders { get; set; } = [];
    }
}
