using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO.Client;

namespace Application.Behavior.Client;

public class UpdateClientCommand : IRequest<ClientDto?>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Telegram { get; set; }
    public string? Address { get; set; }
    public string? Notes { get; set; }
}

public class UpdateClientCommandHandler(IClientRepository repository) : IRequestHandler<UpdateClientCommand, ClientDto?>
{
    public async Task<ClientDto?> Handle(UpdateClientCommand command, CancellationToken cancellationToken)
    {
        ClientEntity? client = await repository.GetByIdAsync(command.Id) ?? throw new Exception($"Клиент с ID {command.Id} не найден");

        client.Name = command.Name;
        client.Phone = command.Phone;
        client.Email = command.Email;
        client.Telegram = command.Telegram;
        client.Address = command.Address;
        client.Notes = command.Notes;
        client.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(client);

        return new ClientDto
        {
            Id = client.Id,
            Name = client.Name,
            Phone = client.Phone,
            Email = client.Email,
            Telegram = client.Telegram,
            Address = client.Address,
            Notes = client.Notes
        };
    }
}