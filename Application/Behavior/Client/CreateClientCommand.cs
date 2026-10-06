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