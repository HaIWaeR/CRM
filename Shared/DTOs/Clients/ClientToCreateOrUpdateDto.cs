namespace Shared.DTOs.Client
{
    /// <summary>
    /// Тело запроса на создание или обновление клиента.
    /// </summary>
    /// <remarks>
    /// Должен быть указан хотя бы один контакт: <see cref="Phone"/>, <see cref="Email"/> или <see cref="Telegram"/>.
    /// Id, статус и даты сюда не входят — их устанавливает сервер.
    /// </remarks>
    public class ClientToCreateOrUpdateDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// Телефон в свободном формате: 10–20 символов из цифр, пробелов, <c>+ - ( )</c>.
        /// Например <c>8-999-123-45-67</c> или <c>+7 (999) 123-45-67</c>.
        /// Сервер приведёт его к виду <c>+7 000 000 00 00</c>.
        /// </summary>
        public string? Phone { get; set; }
        /// <summary>
        /// Email вида <c>name@domain.zone</c>. Должен быть уникальным.
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Имя пользователя Telegram, начинается с <c>@</c>, до 50 символов. Должно быть уникальным.
        /// </summary>
        public string? Telegram { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
    }
}