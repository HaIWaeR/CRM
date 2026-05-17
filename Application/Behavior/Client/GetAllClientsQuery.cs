using Application.DTO.Client;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behavior.Client
{

    public class GetAllClientsQuery : IRequest<List<ClientDto>>;
    public class GetAllClientsQueryHandler(IClientRepository repository) : IRequestHandler<GetAllClientsQuery, List<ClientDto>>
    {
        public async Task<List<ClientDto>> Handle(GetAllClientsQuery query, CancellationToken cancellationToken)
        {
            List<ClientEntity> clients = await repository.GetAllAsync();

            return clients.Select(client => new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                Phone = client.Phone,
                Email = client.Email,
                Telegram = client.Telegram,
                Address = client.Address,
                Notes = client.Notes,
            }).ToList();
        }
    }
}