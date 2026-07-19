using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.Client;

namespace Application.Behavior.Client;

public class GetClientByIdQuery : IRequest<ClientDto>
{
    public Guid Id { get; set; }
}
public class GetClientByIdQueryHandler(IClientRepository repository) : IRequestHandler<GetClientByIdQuery, ClientDto>
{
    public async Task<ClientDto> Handle(GetClientByIdQuery query, CancellationToken cancellationToken)
    {

        ClientEntity? client = await repository.GetByIdAsync(query.Id) 
            ?? throw new KeyNotFoundException($"Клиент с ID {query.Id} не найден");

        ClientDto result = client.Adapt<ClientDto>();
        return result;
    }
}