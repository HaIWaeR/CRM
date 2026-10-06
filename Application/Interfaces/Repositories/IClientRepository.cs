using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    /// <summary>
    /// Репозиторий для хранения и поиска клиентов.
    /// </summary>
    /// <remarks>
    /// Объявлен в слое Application, а реализован в Persistence.
    /// Благодаря этому обработчики команд не зависят от EF Core и конкретной БД,
    /// а в тестах репозиторий можно заменить заглушкой.
    /// </remarks>
    public interface IClientRepository
    {
        // CRUD

        /// <summary>
        /// Добавляет нового клиента и сразу сохраняет изменения в БД.
        /// </summary>
        /// <param name="client">Клиент с заполненными Id, CreatedAt и Status. Не может быть <c>null</c>.</param>
        Task AddAsync(ClientEntity client);

        /// <summary>
        /// Возвращает всех клиентов без фильтрации и пагинации.
        /// </summary>
        /// <returns>Список всех клиентов; пустой список, если клиентов нет.</returns>
        Task<List<ClientEntity>> GetAllAsync();

        /// <summary>
        /// Ищет клиента по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор клиента.</param>
        /// <returns>Найденный клиент или <c>null</c>, если клиента с таким Id нет.</returns>
        Task<ClientEntity?> GetByIdAsync(Guid id);

        /// <summary>
        /// Сохраняет изменения существующего клиента в БД.
        /// </summary>
        /// <param name="client">Клиент с изменёнными данными. Должен существовать в БД.</param>
        /// <returns>Тот же объект клиента после сохранения.</returns>
        Task<ClientEntity> UpdateAsync(ClientEntity client);

        /// <summary>
        /// Удаляет клиента по идентификатору без предварительной загрузки из БД.
        /// </summary>
        /// <param name="id">Идентификатор существующего клиента.</param>
        /// <remarks>
        /// Метод не проверяет существование клиента и наличие у него заказов.
        /// Перед вызовом используйте <see cref="ExistsAsync"/> и <see cref="HasOrdersAsync"/>.
        /// </remarks>
        Task DeleteAsync(Guid id);

        // Дополнительные методы

        /// <summary>
        /// Проверяет, существует ли клиент с указанным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор клиента.</param>
        /// <returns><c>true</c>, если клиент найден; иначе <c>false</c>.</returns>
        Task<bool> ExistsAsync(Guid id);

        /// <summary>
        /// Проверяет, что телефон не занят другим клиентом.
        /// </summary>
        /// <param name="phone">Телефон для проверки. Сравнивается с сохранённым значением по точному совпадению строки.</param>
        /// <param name="excludeId">Id клиента, которого нужно исключить из проверки (при обновлении — сам клиент); <c>null</c> при создании.</param>
        /// <returns><c>true</c>, если телефон свободен; <c>false</c>, если он уже есть у другого клиента.</returns>
        Task<bool> IsPhoneUniqueAsync(string phone, Guid? excludeId = null);

        /// <summary>
        /// Проверяет, что Email не занят другим клиентом. Сравнение без учёта регистра.
        /// </summary>
        /// <param name="email">Email для проверки.</param>
        /// <param name="excludeId">Id клиента, которого нужно исключить из проверки (при обновлении — сам клиент); <c>null</c> при создании.</param>
        /// <returns><c>true</c>, если Email свободен; <c>false</c>, если он уже есть у другого клиента.</returns>
        Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null);

        /// <summary>
        /// Проверяет, что имя пользователя Telegram не занято другим клиентом.
        /// </summary>
        /// <param name="telegramId">Имя пользователя Telegram с <c>@</c>, например <c>@username</c>.</param>
        /// <param name="excludeId">Id клиента, которого нужно исключить из проверки (при обновлении — сам клиент); <c>null</c> при создании.</param>
        /// <returns><c>true</c>, если имя свободно; <c>false</c>, если оно уже есть у другого клиента.</returns>
        Task<bool> IsTelegramUniqueAsync(string telegramId, Guid? excludeId = null);

        // Фильтрация с пагинацией

        /// <summary>
        /// Возвращает одну страницу клиентов, отфильтрованных по заданным условиям.
        /// Все переданные фильтры объединяются через «И».
        /// </summary>
        /// <param name="searchTerm">Текст для поиска по вхождению в имя, фамилию, отчество, телефон или Email (без учёта регистра); <c>null</c> — без поиска.</param>
        /// <param name="status">Статус клиента; <c>null</c> — любой статус.</param>
        /// <param name="phone">Телефон для точного совпадения; <c>null</c> — без фильтра.</param>
        /// <param name="email">Email для точного совпадения без учёта регистра; <c>null</c> — без фильтра.</param>
        /// <param name="telegram">Часть имени Telegram для поиска по вхождению; <c>null</c> — без фильтра.</param>
        /// <param name="page">Номер страницы, начиная с 1.</param>
        /// <param name="size">Количество клиентов на странице.</param>
        /// <returns>Клиенты запрошенной страницы; пустой список, если страница за пределами результатов.</returns>
        /// <example>
        /// Вторая страница активных клиентов, у которых в имени или контактах есть «иван»:
        /// <code>
        /// List&lt;ClientEntity&gt; clients = await repository.GetFilteredAsync(
        ///     searchTerm: "иван",
        ///     status: ClientStatus.Active,
        ///     page: 2,
        ///     size: 20);
        /// </code>
        /// </example>
        Task<List<ClientEntity>> GetFilteredAsync(
            string? searchTerm = null,
            ClientStatus? status = null,
            string? phone = null,
            string? email = null,
            string? telegram = null,
            int page = 1,
            int size = 20);

        // Общее количество записей

        /// <summary>
        /// Считает клиентов, подходящих под фильтры, без учёта пагинации.
        /// Используется вместе с <see cref="GetFilteredAsync"/> для расчёта числа страниц.
        /// </summary>
        /// <param name="searchTerm">Текст для поиска; правила те же, что в <see cref="GetFilteredAsync"/>.</param>
        /// <param name="status">Статус клиента; <c>null</c> — любой статус.</param>
        /// <param name="phone">Телефон для точного совпадения; <c>null</c> — без фильтра.</param>
        /// <param name="email">Email для точного совпадения без учёта регистра; <c>null</c> — без фильтра.</param>
        /// <param name="telegram">Часть имени Telegram; <c>null</c> — без фильтра.</param>
        /// <returns>Общее количество подходящих клиентов.</returns>
        Task<int> GetTotalCountAsync(
            string? searchTerm = null,
            ClientStatus? status = null,
            string? phone = null,
            string? email = null,
            string? telegram = null);

        // Проверка связей

        /// <summary>
        /// Проверяет, есть ли у клиента заказы. Используется перед удалением.
        /// </summary>
        /// <param name="clientId">Идентификатор клиента.</param>
        /// <returns><c>true</c>, если у клиента есть хотя бы один заказ; иначе <c>false</c>.</returns>
        Task<bool> HasOrdersAsync(Guid clientId);
    }
}