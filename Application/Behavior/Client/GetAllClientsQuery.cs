using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behavior.Client
{

    public class GetAllClientsQuery : IRequest<List<ClientEntity>>;
    public class GetAllClientsQueryHandler(IClientRepository repository) : IRequestHandler<GetAllClientsQuery, List<ClientEntity>>
    {
        public async Task<List<ClientEntity>> Handle(GetAllClientsQuery query, CancellationToken cancellationToken)
        {
            List<ClientEntity> clients = await repository.GetAllAsync();

            return clients;
        }
    }
}