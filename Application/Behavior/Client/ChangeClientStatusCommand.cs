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