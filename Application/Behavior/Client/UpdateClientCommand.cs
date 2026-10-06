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