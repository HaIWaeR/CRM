using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behavior.Client;

public class GetClientByIdQuery : IRequest<ClientEntity?>
{
    public Guid Id { get; set; }
}
public class GetClientByIdQueryHandler(IClientRepository repository) : IRequestHandler<GetClientByIdQuery, ClientEntity?>
{
    public async Task<ClientEntity?> Handle(GetClientByIdQuery query, CancellationToken cancellationToken)
    {
        ClientEntity? client = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Клиент с ID {query.Id} не найден");

        return client;
    }
}