using Application.Helpers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.Client;

namespace Application.Behavior.Client;

public class UpdateClientCommand : IRequest<ClientDto>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Telegram { get; set; }
    public string? Address { get; set; }
    public string? Notes { get; set; }
}

public class UpdateClientCommandHandler(IClientRepository repository) : IRequestHandler<UpdateClientCommand, ClientDto>
{
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