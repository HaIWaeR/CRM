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

        if (!string.IsNullOrWhiteSpace(command.Email) && command.Email != client.Email)
        {
            ClientEntity? existing = await repository.GetByEmailAsync(command.Email);
            if (existing != null)
                throw new Exception("Клиент с таким email уже существует");
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