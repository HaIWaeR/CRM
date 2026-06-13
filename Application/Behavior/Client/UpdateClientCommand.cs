using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.Client;

public class UpdateClientCommand : IRequest<ClientEntity?>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Telegram { get; set; }
    public string? Address { get; set; }
    public string? Notes { get; set; }
}

public class UpdateClientCommandHandler(IClientRepository repository) : IRequestHandler<UpdateClientCommand, ClientEntity?>
{
    public async Task<ClientEntity?> Handle(UpdateClientCommand command, CancellationToken cancellationToken)
    {
        ClientEntity? client = await repository.GetByIdAsync(command.Id)
        ?? throw new Exception($"Клиент с ID {command.Id} не найден");

        if (string.IsNullOrWhiteSpace(command.Phone) &&
            string.IsNullOrWhiteSpace(command.Email) &&
            string.IsNullOrWhiteSpace(command.Telegram))
        {
            throw new Exception("Необходимо указать хотя бы один контакт: телефон, email или telegram.");
        }

        if (!string.IsNullOrWhiteSpace(command.Email))
        {
            ClientEntity? email = await repository.GetByEmailAsync(command.Email);
            if (email != null)
                throw new Exception("Клиент с таким email уже существует.");
        }

        if (!string.IsNullOrWhiteSpace(command.Phone))
        {
            ClientEntity? phone = await repository.GetByPhoneAsync(command.Phone);
            if (phone != null)
                throw new Exception("Клиент с таким номером телефона уже существует.");
        }

        if (!string.IsNullOrWhiteSpace(command.Telegram))
        {
            ClientEntity? telegram = await repository.GetByTelegramAsync(command.Telegram);
            if (telegram != null)
                throw new Exception("Клиент с таким Telegram уже существует.");
        }

        client.Name = command.Name;
        client.Phone = command.Phone;
        client.Email = command.Email;
        client.Telegram = command.Telegram;
        client.Address = command.Address;
        client.Notes = command.Notes;
        client.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(client);

        return client;
    }
}