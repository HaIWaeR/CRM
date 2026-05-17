using Application.DTO.Client;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behavior.Client;

public class GetClientByIdQuery : IRequest<ClientDto?>
{
    public Guid Id { get; set; }
}
public class GetClientByIdQueryHandler(IClientRepository repository) : IRequestHandler<GetClientByIdQuery, ClientDto?>
{
    public async Task<ClientDto?> Handle(GetClientByIdQuery query, CancellationToken cancellationToken)
    {
        ClientEntity? client = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Клиент с ID {query.Id} не найден");

        return new ClientDto
        {
            Id = client.Id,
            Name = client.Name,
            Phone = client.Phone,
            Email = client.Email,
            Telegram = client.Telegram,
            Address = client.Address,
            Notes = client.Notes,
        };
    }
}