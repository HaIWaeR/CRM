# Исключения из тз
[1.1]
[1.2]
В тз просили сделать на весь проект, но отказался от этой задумке по нескольким причинам. Дипломный проект на данный момент включает в себя 349 классов, из которых буличные около 290. На данный момент не успею задокументировать весь проект но обязательно сделаю в дальнейшем. Помогло найти несколько ошибок в коде не заметных ранее + упрощает работу в награмаждённном проекте. 

Данное задание применил на всю цепочку связанную с логикой Client, дальнешие классы используют такой же патерy, в случае путаниц можно будет посомтреть на эту сущьность и еёё проработку и применить на других деталях проекта

- ClientEntity
- ClientStatus
- IClientRepository
- ClientRepository
- ClientConfiguration
- ApplicationContext (Посредние)
- ClientDto
- PaginatedResult (Посредние)
- CreateClientCommand, UpdateClientCommand, ChangeClientStatusCommand, DeleteClientCommand, GetClientByIdQuery, GetAllClientsQuery
- ChangeClientStatusCommandValidatorTests, CreateClientCommandValidatorTests, UpdateClientCommandValidatorTests
- ValidationPipelineBehavior (Посредние)
- PhoneHelper (Посредние)

# Небольшое руководство для применения документации патерном к другим сущьностям 

### Включение генерации документации
```xml
<PropertyGroup>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
  <NoWarn>$(NoWarn);CS1591</NoWarn>
</PropertyGroup>
```

GenerateDocumentationFile — при сборке рядом с .dll появляется .xml с комментариями.
NoWarn CS1591 — глушит предупреждения «нет комментария». Нужен, пока задокументирована только часть сущностей. Чтобы проверить себя, временно убери строку и собери проект.

### Используемые теги
| Тег | Где и зачем |
|---|---|
| `<summary>` | Что это и зачем. Есть у каждого публичного класса, метода, свойства. |
| `<remarks>` | Неочевидное: архитектурные решения, ограничения, кто вызывает. |
| `<param name="x">` | Каждый параметр: допустимые значения и что значит `null`. |
| `<typeparam name="T">` | Параметр generic-типа (`PaginatedResult<T>`, `ValidationPipelineBehavior<,>`). |
| `<returns>` | Смысл результата, а не тип: «`null`, если не найден». |
| `<exception cref="...">` | Тип исключения и условие, при котором оно выбрасывается. |
| `<example>` + `<code>` | Пример вызова (обязателен хотя бы один на проект). |
| `<see cref="..."/>` | Ссылка на класс или член; в IntelliSense становится кликабельной. |
| `<paramref name="x"/>` | Ссылка на параметр внутри текста. |
| `<typeparamref name="T"/>` | Ссылка на generic-параметр внутри текста. |
| `<c>...</c>` | Короткий код или значение в тексте: `<c>null</c>`, `<c>true</c>`. |
| `<list type="bullet">` + `<item>` | Маркированный список внутри `<remarks>`. |
| `<inheritdoc/>` | Взять комментарий из интерфейса или базового класса. |
| `<inheritdoc cref="X.Y"/>` | Взять комментарий у любого указанного члена. |

### Приёмы, чтобы не дублировать текст
- Интерфейс + реализация. Полностью документируется интерфейс (`IClientRepository`), а в реализации (`ClientRepository`) над каждым методом стоит /// <inheritdoc/>.
- DTO, повторяющий сущность. Над свойствами `ClientDto` стоит /// <inheritdoc cref="ClientEntity.Phone"/>, и текст берётся из сущности.
- Команда, повторяющая входной DTO. Свойства `CreateClientCommand` ссылаются на `ClientToCreateOrUpdateDto` через `inheritdoc cref`.
- Входной DTO описывается отдельно, потому что он отличается от сущности: телефон в свободном формате, нет Id и дат.

### Правила, которым придерживался из LXP и Хабр

- summary начинается с глагола: «Возвращает…», «Проверяет…», «Создаёт…».

- Не повторять имя: вместо «Удаляет клиента» писать, что проверяется и что нельзя.

- В param указывать ограничения: длину, формат, что значит null.

- exception ставится там, где исключение реально выбрасывается:

- KeyNotFoundException, InvalidOperationException — у Handle обработчика;

- ValidationException — у ValidationPipelineBehavior.Handle, а у команды только упоминание в <remarks> (обработчик её не бросает);

- исключения EF Core (DbUpdateConcurrencyException) — в реализации репозитория, потому что слой Application не ссылается на EF Core и cref там не разрешится.

- Параметры primary-конструктора (class X(IRepo repository)) описываются через <param> в комментарии к классу.

- Валидаторы: правила не перечисляются (они видны в коде), описывается только назначение.

- Незаполняемые поля помечаются честно: «Зарезервировано, в текущей версии не заполняется».

- В code символы < и > экранируются: List&lt;ClientEntity&gt;.

### Порядок работы с новой сущностью
1. Domain: enum и сущность.
2. Интерфейс репозитория (полностью, с <example>).
3. Реализация репозитория (<inheritdoc/> + собственные <exception>), конфигурация EF, DbSet в контексте.
4. DTO.
5. Команды/запросы и обработчики.
6. Валидаторы.
7. Контроллер (в <summary> метода указываются HTTP-метод, маршрут и роли).

# Документирование публичного API на Client


### ClientEntity
```c#
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

```

### ClientStatus
```c#
using System.Runtime.Intrinsics.X86;

namespace Domain.Enums
{
    /// <summary>
    ///  Статус клиента в CRM, определяющий его текущее состояние в работе с компанией
    /// </summary>
    /// <remarks>
    /// При создании через API клиент получает статус<see cref = "Active" />.
    /// Значения начинаются с 1, поэтому 0 считается недопустимым статусом
    /// и отклоняется валидатором.
    /// </remarks>>
    public enum ClientStatus
    {
        /// <summary>
        ///  Клиент работает с компанией и может оформлять заказы.
        /// </summary>
        Active = 1,
        /// <summary>
        /// Клиент давно не появлялся, прекратил сотрудничество или заблокирован.
        /// Запись сохраняется для истории заказов.
        /// </summary>
        Inactive = 2,
        /// <summary>
        /// Черновик: клиент заведён впопыхах (например, пришёл со срочным заказом),
        /// но его данные ещё не проверены и не утверждены.
        /// </summary>
        Draft = 3
    }
}
```

### IClientRepository
```c#
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
```

### ClientRepository
```c#
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

```

### ClientConfiguration
```c#
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    /// <summary>
    /// Настраивает отображение <see cref="ClientEntity"/> на таблицу <c>Clients</c>:
    /// первичный ключ, обязательные поля и максимальную длину строк.
    /// </summary>
    /// <remarks>
    /// Ограничения длины совпадают с правилами валидаторов команд Client,
    /// поэтому при изменении одного нужно менять и другое.
    /// </remarks>
    public class ClientConfiguration : IEntityTypeConfiguration<ClientEntity>
    {
        /// <summary>
        /// Применяет настройки сущности клиента к модели EF Core.
        /// </summary>
        /// <param name="builder">Построитель конфигурации сущности, передаётся EF Core.</param>
        public void Configure(EntityTypeBuilder<ClientEntity> builder)
        {
            builder.ToTable("Clients");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);
            builder.Property(x => x.MiddleName).HasMaxLength(100);
            builder.Property(x => x.Phone).HasMaxLength(20);
            builder.Property(x => x.Email).HasMaxLength(100);
            builder.Property(x => x.Telegram).HasMaxLength(50);
            builder.Property(x => x.Address).HasMaxLength(300);
            builder.Property(x => x.Notes).HasMaxLength(500);
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(ClientStatus.Active);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired(false);
        }
    }
}
```

### ApplicationContext
```c#
using Domain.Entities;
using Domain.Entities.Supporting;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;

namespace Persistence
{
    /// <summary>
    /// Контекст EF Core для работы с базой данных CRM (PostgreSQL).
    /// Содержит наборы всех сущностей и применяет их конфигурации.
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Создаёт контекст с параметрами подключения, заданными при регистрации в DI.
        /// </summary>
        /// <param name="options">Параметры контекста, включая строку подключения <c>DefaultConnection</c>.</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<BranchEntity> Branches { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<MaterialEntity> Materials { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<StockItemEntity> StockItems { get; set; }
        public DbSet<StorageZoneEntity> Storages { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<WarehouseRoomEntity> Warehouses { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<SupplierMaterialEntity> SupplierMaterials { get; set; }
        public DbSet<SupplierEntity> Suppliers { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BranchConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new StockItemConfiguration());
            modelBuilder.ApplyConfiguration(new StorageZoneConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseRoomConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
        }
    }
}

```

### ClientDto
```c#
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
```

### PaginatedResult
```c#
namespace Shared.DTOs.Pagination
{
    /// <summary>
    /// Одна страница результатов списочного запроса вместе с данными для навигации по страницам.
    /// </summary>
    /// <typeparam name="T">Тип элементов на странице, например <c>ClientDto</c>.</typeparam>
    public class PaginatedResult<T>
    {
        /// <summary>
        /// Элементы текущей страницы. Пустой список, если страница за пределами результатов.
        /// </summary>
        public List<T> Items { get; set; } = [];

        /// <summary>
        /// Общее количество элементов, подходящих под фильтры, по всем страницам.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Номер текущей страницы, начиная с 1.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Максимальное количество элементов на странице.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Общее количество страниц; 0, если элементов нет.
        /// </summary>
        public int TotalPages { get; set; }
    }
}
```

## CreateClientCommand
```c#
using Application.Helpers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Behavior.Client
{
    /// <summary>
    /// Команда на создание нового клиента. Возвращает Id созданного клиента.
    /// </summary>
    /// <remarks>
    /// Перед обработкой команда проверяется <see cref="Validators.Client.CreateClientCommandValidator"/>
    /// в <see cref="PipelineBehaviors.ValidationPipelineBehavior{TRequest, TResponse}"/>;
    /// при ошибках выбрасывается <see cref="FluentValidation.ValidationException"/> и обработчик не вызывается.
    /// </remarks>
    public class CreateClientCommand : IRequest<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        /// <inheritdoc cref="ClientToCreateOrUpdateDto.Phone"/>
        public string? Phone { get; set; }
        /// <inheritdoc cref="ClientToCreateOrUpdateDto.Email"/>
        public string? Email { get; set; }
        /// /// <inheritdoc cref="ClientToCreateOrUpdateDto.Telegram"/>
        public string? Telegram { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
    }

    /// <summary>
    /// Обработчик <see cref="CreateClientCommand"/>: проверяет уникальность контактов,
    /// нормализует телефон и сохраняет клиента со статусом <see cref="ClientStatus.Active"/>.
    /// </summary>
    /// <param name="repository">Репозиторий клиентов.</param>
    public class CreateClientCommandHandler(IClientRepository repository) : IRequestHandler<CreateClientCommand, Guid>
    {
        /// <summary>
        /// Создаёт клиента и сохраняет его в БД.
        /// </summary>
        /// <param name="command">Данные нового клиента, уже прошедшие валидацию.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Идентификатор созданного клиента.</returns>
        /// <exception cref="InvalidOperationException">
        /// Не указан ни один контакт, либо телефон, Email или Telegram уже заняты другим клиентом.
        /// </exception>
        public async Task<Guid> Handle(CreateClientCommand command, CancellationToken cancellationToken)
        {
            bool hasContact = !string.IsNullOrWhiteSpace(command.Phone) ||
                              !string.IsNullOrWhiteSpace(command.Email) ||
                              !string.IsNullOrWhiteSpace(command.Telegram);
            if (!hasContact)
                throw new InvalidOperationException("У клиента должен быть указан хотя бы один контакт (телефон, Email или Telegram)");

            if (!string.IsNullOrWhiteSpace(command.Phone))
            {
                bool phoneUnique = await repository.IsPhoneUniqueAsync(command.Phone);
                if (!phoneUnique)
                    throw new InvalidOperationException("Клиент с таким телефоном уже существует");
            }

            if (!string.IsNullOrWhiteSpace(command.Email))
            {
                bool emailUnique = await repository.IsEmailUniqueAsync(command.Email);
                if (!emailUnique)
                    throw new InvalidOperationException("Клиент с таким Email уже существует");
            }

            if (!string.IsNullOrWhiteSpace(command.Telegram))
            {
                bool telegramUnique = await repository.IsTelegramUniqueAsync(command.Telegram);
                if (!telegramUnique)
                    throw new InvalidOperationException("Клиент с таким Telegram уже существует");
            }

            ClientEntity client = command.Adapt<ClientEntity>();
            client.Phone = PhoneHelper.FormatPhone(command.Phone);
            client.Id = Guid.NewGuid();
            client.CreatedAt = DateTime.UtcNow;
            client.Status = ClientStatus.Active;

            await repository.AddAsync(client);
            return client.Id;
        }
    }
}
```
## UpdateClientCommand
```c#
using Application.Helpers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.Client;

namespace Application.Behavior.Client;

/// <summary>
/// Команда на полное обновление данных клиента. Возвращает обновлённого клиента.
/// </summary>
/// <remarks>
/// Работает как полная замена (PUT): поля, переданные пустыми, будут очищены.
/// Статус клиента этой командой не меняется — для этого есть <see cref="ChangeClientStatusCommand"/>.
/// Перед обработкой проверяется <see cref="Validators.Client.UpdateClientCommandValidator"/>;
/// при ошибках выбрасывается <see cref="FluentValidation.ValidationException"/>.
/// </remarks>
public class UpdateClientCommand : IRequest<ClientDto>
{

    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public DateTime? BirthDate { get; set; }
    /// <inheritdoc cref="ClientToCreateOrUpdateDto.Phone"/>
    public string? Phone { get; set; }
    /// <inheritdoc cref="ClientToCreateOrUpdateDto.Email"/>
    public string? Email { get; set; }
    /// <inheritdoc cref="ClientToCreateOrUpdateDto.Telegram"/>
    public string? Telegram { get; set; }
    public string? Address { get; set; }
    public string? Notes { get; set; }
}
/// <summary>
/// Обработчик <see cref="UpdateClientCommand"/>: проверяет существование клиента
/// и уникальность контактов, затем перезаписывает его данные.
/// </summary>
/// <param name="repository">Репозиторий клиентов.</param>
public class UpdateClientCommandHandler(IClientRepository repository) : IRequestHandler<UpdateClientCommand, ClientDto>
{
    /// <summary>
    /// Обновляет данные клиента и устанавливает <see cref="ClientEntity.UpdatedAt"/>.
    /// </summary>
    /// <param name="command">Новые данные клиента и его Id.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Клиент после обновления.</returns>
    /// <exception cref="KeyNotFoundException">Клиент с указанным Id не найден.</exception>
    /// <exception cref="InvalidOperationException">
    /// Не указан ни один контакт, либо телефон, Email или Telegram заняты другим клиентом.
    /// </exception>
    public async Task<ClientDto> Handle(UpdateClientCommand command, CancellationToken cancellationToken)
    {
        ClientEntity? existing = await repository.GetByIdAsync(command.Id)
            ?? throw new KeyNotFoundException($"Клиент с ID {command.Id} не найден");

        bool hasContact = !string.IsNullOrWhiteSpace(command.Phone) ||
                              !string.IsNullOrWhiteSpace(command.Email) ||
                              !string.IsNullOrWhiteSpace(command.Telegram);

        if (!hasContact)
            throw new InvalidOperationException("У клиента должен быть указан хотя бы один контакт (телефон, Email или Telegram)");


        if (!string.IsNullOrWhiteSpace(command.Phone))
        {
            bool phoneUnique = await repository.IsPhoneUniqueAsync(command.Phone, command.Id);
            if (!phoneUnique)
                throw new InvalidOperationException("Клиент с таким телефоном уже существует");
        }

        if (!string.IsNullOrWhiteSpace(command.Email))
        {
            bool emailUnique = await repository.IsEmailUniqueAsync(command.Email, command.Id);
            if (!emailUnique)
                throw new InvalidOperationException("Клиент с таким Email уже существует");
        }

        if (!string.IsNullOrWhiteSpace(command.Telegram))
        {
            bool telegramUnique = await repository.IsTelegramUniqueAsync(command.Telegram, command.Id);
            if (!telegramUnique)
                throw new InvalidOperationException("Клиент с таким Telegram уже существует");
        }

        command.Adapt(existing);
        existing.Phone = PhoneHelper.FormatPhone(command.Phone);
        existing.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(existing);

        ClientDto result = existing.Adapt<ClientDto>();
        return result;
    }
}
```
## ChangeClientStatusCommand
```c#
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Client
{
    /// <summary>
    /// Команда на смену статуса клиента. Возвращает <c>true</c> при успехе.
    /// </summary>
    /// <remarks>
    /// Статус проверяется <see cref="Validators.ChangeClientStatusCommandValidator"/>;
    /// при недопустимом значении выбрасывается <see cref="FluentValidation.ValidationException"/>.
    /// </remarks>
    public class ChangeClientStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public ClientStatus Status { get; set; }
    }

    /// <summary>
    /// Обработчик <see cref="ChangeClientStatusCommand"/>.
    /// </summary>
    /// <param name="repository">Репозиторий клиентов.</param>
    public class ChangeClientStatusCommandHandler(IClientRepository repository) : IRequestHandler<ChangeClientStatusCommand, bool>
    {
        /// <summary>
        /// Устанавливает клиенту новый статус и обновляет <see cref="ClientEntity.UpdatedAt"/>,
        /// даже если статус не изменился.
        /// </summary>
        /// <param name="command">Id клиента и новый статус.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Всегда <c>true</c>; при ошибке выбрасывается исключение.</returns>
        /// <exception cref="KeyNotFoundException">Клиент с указанным Id не найден.</exception>
        public async Task<bool> Handle(ChangeClientStatusCommand command, CancellationToken cancellationToken)
        {
            ClientEntity? client = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Клиент с ID {command.Id} не найден");

            client.Status = command.Status;
            client.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(client);
            return true;
        }
    }
}
```
## DeleteClientCommand
```c#
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Behavior.Client;

/// <summary>
/// Команда на удаление клиента. Возвращает <c>true</c> при успехе.
/// </summary>
/// <remarks>
/// Удаление физическое — запись исчезает из БД. Клиента с заказами удалить нельзя;
/// вместо этого его можно перевести в статус <see cref="Domain.Enums.ClientStatus.Inactive"/>.
/// </remarks>
public class DeleteClientCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

/// <summary>
/// Обработчик <see cref="DeleteClientCommand"/>.
/// </summary>
/// <param name="repository">Репозиторий клиентов.</param>
public class DeleteClientCommandHandler(IClientRepository repository) : IRequestHandler<DeleteClientCommand, bool>
{
    public async Task<bool> Handle(DeleteClientCommand command, CancellationToken cancellationToken)
    {
        if(!await repository.ExistsAsync(command.Id))
            throw new KeyNotFoundException($"Клиент с ID {command.Id} не найден");

        if (await repository.HasOrdersAsync(command.Id))
            throw new InvalidOperationException("Нельзя удалить клиента, у которого есть заказы");

        await repository.DeleteAsync(command.Id);
        return true;
    }
}   
```
## GetClientByIdQuery
```c#
using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.Client;

namespace Application.Behavior.Client;

/// <summary>
/// Запрос на получение одного клиента по идентификатору.
/// </summary>
public class GetClientByIdQuery : IRequest<ClientDto>
{
    public Guid Id { get; set; }
}
/// <summary>
/// Обработчик <see cref="GetClientByIdQuery"/>.
/// </summary>
/// <param name="repository">Репозиторий клиентов.</param>
public class GetClientByIdQueryHandler(IClientRepository repository) : IRequestHandler<GetClientByIdQuery, ClientDto>
{
    /// <summary>
    /// Загружает клиента из БД и преобразует его в <see cref="ClientDto"/>.
    /// </summary>
    /// <param name="query">Id искомого клиента.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Данные найденного клиента.</returns>
    /// <exception cref="KeyNotFoundException">Клиент с указанным Id не найден.</exception>
    public async Task<ClientDto> Handle(GetClientByIdQuery query, CancellationToken cancellationToken)
    {

        ClientEntity? client = await repository.GetByIdAsync(query.Id) 
            ?? throw new KeyNotFoundException($"Клиент с ID {query.Id} не найден");

        ClientDto result = client.Adapt<ClientDto>();
        return result;
    }
}
```
## GetAllClientsQuery
```c#
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.Client;
using Shared.DTOs.Pagination;

namespace Application.Behavior.Client
{
    /// <summary>
    /// Запрос на получение страницы клиентов с фильтрацией и поиском.
    /// </summary>
    /// <remarks>
    /// Правила фильтров описаны в <see cref="IClientRepository.GetFilteredAsync"/>.
    /// </remarks>
    public class GetAllClientsQuery : IRequest<PaginatedResult<ClientDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public string? SearchTerm { get; set; }
        public ClientStatus? Status { get; set; }
        /// <summary>
        /// Фильтр по точному совпадению телефона; <c>null</c> — без фильтра.
        /// </summary>
        public string? Phone { get; set; }
        /// <summary>
        /// Фильтр по точному совпадению Email без учёта регистра; <c>null</c> — без фильтра.
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Фильтр по части имени Telegram; <c>null</c> — без фильтра.
        /// </summary>
        public string? Telegram { get; set; }
    }
    /// <summary>
    /// Обработчик <see cref="GetAllClientsQuery"/>.
    /// </summary>
    /// <param name="repository">Репозиторий клиентов.</param>
    public class GetAllClientsQueryHandler(IClientRepository repository) : IRequestHandler<GetAllClientsQuery, PaginatedResult<ClientDto>>
    {
        /// <summary>
        /// Загружает запрошенную страницу клиентов и считает общее количество подходящих записей.
        /// </summary>
        /// <param name="query">Номер страницы, её размер и фильтры.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>
        /// Страница клиентов с общим количеством записей и страниц.
        /// Если номер страницы больше их общего числа, <c>Items</c> будет пустым,
        /// но <c>TotalCount</c> и <c>TotalPages</c> заполнятся.
        /// </returns>
        public async Task<PaginatedResult<ClientDto>> Handle(GetAllClientsQuery query, CancellationToken cancellationToken)
        {
            List<ClientEntity> clients = await repository.GetFilteredAsync(
                query.SearchTerm,
                query.Status,
                query.Phone,
                query.Email,
                query.Telegram,
                query.Page,
                query.Size);

            int totalCount = await repository.GetTotalCountAsync(
                query.SearchTerm,
                query.Status,
                query.Phone,
                query.Email,
                query.Telegram);

            int totalPages = (int)Math.Ceiling(totalCount / (double)query.Size);

            if (query.Page > totalPages && totalPages > 0)
            {
                return new PaginatedResult<ClientDto>
                {
                    Items = new List<ClientDto>(),
                    TotalCount = totalCount,
                    Page = query.Page,
                    Size = query.Size,
                    TotalPages = totalPages
                };
            }

            List<ClientDto> items = clients.Adapt<List<ClientDto>>();

            return new PaginatedResult<ClientDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = query.Page,
                Size = query.Size,
                TotalPages = totalPages
            };
        }
    }
}
```
## ChangeClientStatusCommandValidatorTests
```c#
using FluentValidation;
using Application.Behavior.Client;

namespace Application.Validators
{
    public class ChangeClientStatusCommandValidator : AbstractValidator<ChangeClientStatusCommand>
    {
        /// <summary>
        /// Задаёт правило: статус должен быть одним из значений <see cref="ClientStatus"/>.
        /// </summary>
        public ChangeClientStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Указан недопустимый статус филиала.");
        }
    }
}

```
## CreateClientCommandValidatorTests
```c#
using Application.Behavior.Branch;
using Application.Behavior.Client;
using FluentValidation;

namespace Application.Validators.Client
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        /// <summary>
        /// Задаёт правила: обязательное имя, ограничения длины, формат телефона, Email и Telegram,
        /// дата рождения не в будущем, хотя бы один контакт.
        /// </summary>
        public CreateClientCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Имя обязательно")
                .MaximumLength(100).WithMessage("Максимум 100 символов");

            RuleFor(x => x.LastName)
                .MaximumLength(100).WithMessage("Максимум 100 символов")
                .When(x => !string.IsNullOrEmpty(x.LastName));

            RuleFor(x => x.MiddleName)
                .MaximumLength(100).WithMessage("Максимум 100 символов")
                .When(x => !string.IsNullOrEmpty(x.MiddleName));

            RuleFor(x => x.Phone)
                .Matches(@"^\+?[0-9\s\-\(\)]{10,20}$")
                .WithMessage("Некорректный номер телефона")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.Email)
                .Must(x => string.IsNullOrEmpty(x) ||
                           (x.Contains("@") &&
                            x.Contains(".") &&
                            x.Split('@')[0].Length > 0 &&
                            x.Split('@')[1].Length > 0 &&
                            x.Split('@')[1].Contains(".") &&
                            x.Split('@')[1].Split('.')[1].Length > 0))
                .WithMessage("Некорректный Email")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Telegram)
                .Must(x => string.IsNullOrEmpty(x) || x.StartsWith("@"))
                .WithMessage("Telegram должен начинаться с @")
                .MaximumLength(50).WithMessage("Максимум 50 символов")
                .When(x => !string.IsNullOrEmpty(x.Telegram));

            RuleFor(x => x.Address)
                .MaximumLength(300).WithMessage("Максимум 300 символов")
                .When(x => !string.IsNullOrEmpty(x.Address));

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Максимум 500 символов")
                .When(x => !string.IsNullOrEmpty(x.Notes));

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.UtcNow).WithMessage("Дата рождения не может быть в будущем")
                .When(x => x.BirthDate.HasValue);

            RuleFor(x => x)
                .Must(x => !string.IsNullOrWhiteSpace(x.Phone) ||
                           !string.IsNullOrWhiteSpace(x.Email) ||
                           !string.IsNullOrWhiteSpace(x.Telegram))
                .WithMessage("Укажите хотя бы один контакт (телефон, Email или Telegram)");
        }
    }
}

```
## UpdateClientCommandValidatorTests
```c#

using Application.Behavior.Client;
using FluentValidation;

namespace Application.Validators.Client
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        /// <summary>
        /// Задаёт правила: обязательное имя, ограничения длины, формат телефона, Email и Telegram,
        /// дата рождения не в будущем, хотя бы один контакт.
        /// </summary>
        public UpdateClientCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Имя обязательно")
                .MaximumLength(100).WithMessage("Максимум 100 символов");

            RuleFor(x => x.LastName)
                .MaximumLength(100).WithMessage("Максимум 100 символов")
                .When(x => !string.IsNullOrEmpty(x.LastName));

            RuleFor(x => x.MiddleName)
                .MaximumLength(100).WithMessage("Максимум 100 символов")
                .When(x => !string.IsNullOrEmpty(x.MiddleName));

            RuleFor(x => x.Phone)
                .Matches(@"^\+?[0-9\s\-\(\)]{10,20}$")
                .WithMessage("Некорректный номер телефона")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.Email)
                .Must(x => string.IsNullOrEmpty(x) ||
                           (x.Contains("@") &&
                            x.Contains(".") &&
                            x.Split('@')[0].Length > 0 &&
                            x.Split('@')[1].Length > 0 &&
                            x.Split('@')[1].Contains(".") &&
                            x.Split('@')[1].Split('.')[1].Length > 0))
                .WithMessage("Некорректный Email")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Telegram)
                .Must(x => string.IsNullOrEmpty(x) || x.StartsWith("@"))
                .WithMessage("Telegram должен начинаться с @")
                .MaximumLength(50).WithMessage("Максимум 50 символов")
                .When(x => !string.IsNullOrEmpty(x.Telegram));

            RuleFor(x => x.Address)
                .MaximumLength(300).WithMessage("Максимум 300 символов")
                .When(x => !string.IsNullOrEmpty(x.Address));

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Максимум 500 символов")
                .When(x => !string.IsNullOrEmpty(x.Notes));

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.UtcNow).WithMessage("Дата рождения не может быть в будущем")
                .When(x => x.BirthDate.HasValue);

            RuleFor(x => x)
                .Must(x => !string.IsNullOrWhiteSpace(x.Phone) ||
                           !string.IsNullOrWhiteSpace(x.Email) ||
                           !string.IsNullOrWhiteSpace(x.Telegram))
                .WithMessage("Укажите хотя бы один контакт (телефон, Email или Telegram)");
        }
    }
}

```
### ValidationPipelineBehavior
```c#
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.PipelineBehaviors
{
    /// <summary>
    /// Шаг конвейера MediatR, который перед вызовом обработчика прогоняет запрос
    /// через все зарегистрированные для него валидаторы FluentValidation.
    /// </summary>
    /// <remarks>
    /// Подключается в <c>Program.cs</c> через <c>AddOpenBehavior</c> и срабатывает для каждого запроса.
    /// Если для запроса нет валидаторов (например, для <see cref="Behavior.Client.GetClientByIdQuery"/>),
    /// запрос сразу передаётся обработчику.
    /// </remarks>
    /// <typeparam name="TRequest">Тип команды или запроса.</typeparam>
    /// <typeparam name="TResponse">Тип результата обработчика.</typeparam>
    /// <param name="validators">Все валидаторы для <typeparamref name="TRequest"/>, найденные в DI.</param>
    public class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Запускает все валидаторы параллельно и собирает ошибки.
        /// Если ошибок нет — вызывает следующий шаг конвейера.
        /// </summary>
        /// <param name="request">Проверяемая команда или запрос.</param>
        /// <param name="next">Следующий шаг конвейера (как правило, сам обработчик).</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат обработчика.</returns>
        /// <exception cref="ValidationException">
        /// Хотя бы один валидатор нашёл ошибку; исключение содержит список всех ошибок.
        /// </exception>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!validators.Any())
                return await next();

            ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);

            ValidationResult[] validationResults = await Task.WhenAll(
                validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            List<ValidationFailure> failures = validationResults
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
```
### PhoneHelper
```c#
namespace Application.Helpers
{
    /// <summary>
    /// Вспомогательные методы для работы с номерами телефонов.
    /// </summary>
    public static class PhoneHelper
    {
        /// <summary>
        /// Приводит российский номер к виду <c>+7 000 000 00 00</c>.
        /// </summary>
        /// <remarks>
        /// Из строки удаляются все символы, кроме цифр и <c>+</c>. Затем:
        /// <list type="bullet">
        /// <item>11 цифр с ведущей <c>8</c> — <c>8</c> заменяется на <c>+7</c>;</item>
        /// <item>10 цифр — добавляется <c>+7</c>;</item>
        /// <item><c>+7</c> и 10 цифр — только расставляются пробелы.</item>
        /// </list>
        /// Номер, не подходящий ни под одно правило (иностранный, неполный), возвращается
        /// без форматирования — только цифры и <c>+</c>.
        /// </remarks>
        /// <param name="phone">Номер в произвольном формате; может быть <c>null</c> или пустым.</param>
        /// <returns>
        /// Отформатированный номер; номер из цифр без форматирования, если он не распознан как российский;
        /// <c>null</c>, если строка пустая или в ней нет цифр.
        /// </returns>
        /// <example>
        /// <code>
        /// PhoneHelper.FormatPhone("8 (999) 123-45-67"); // "+7 999 123 45 67"
        /// PhoneHelper.FormatPhone("9991234567");        // "+7 999 123 45 67"
        /// PhoneHelper.FormatPhone("  ");                // null
        /// </code>
        /// </example>
        public static string? FormatPhone(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return null;

            string digits = new string(phone.Where(c => char.IsDigit(c) || c == '+').ToArray());

            if (digits.Length == 0)
                return null;

            if (digits.StartsWith("+"))
            {
                if (digits.Length == 12 && digits.StartsWith("+7"))
                    return $"{digits[..2]} {digits[2..5]} {digits[5..8]} {digits[8..10]} {digits[10..12]}";

                return digits;
            }

            if (digits.Length == 11 && digits.StartsWith("8"))
            {
                digits = "+7" + digits[1..];
                return $"+7 {digits[2..5]} {digits[5..8]} {digits[8..10]} {digits[10..12]}";
            }

            if (digits.Length == 10)
            {
                digits = "+7" + digits;
                return $"+7 {digits[2..5]} {digits[5..8]} {digits[8..10]} {digits[10..12]}";
            }

            return digits;
        }
    }
}

```

# Диаграмма последовательности
