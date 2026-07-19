using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.Client;

namespace Application.Behavior.Client
{
    public class GetAllClientsQuery : IRequest<List<ClientDto>>
    {
        public string? SearchTerm { get; set; }
        public bool? IsActive { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Telegram { get; set; }
    }
    public class GetAllClientsQueryHandler(IClientRepository repository) : IRequestHandler<GetAllClientsQuery, List<ClientDto>>
    {
        public async Task<List<ClientDto>> Handle(GetAllClientsQuery query, CancellationToken cancellationToken)
        {
            List<ClientEntity> clients = await repository.GetFilteredAsync(
                query.SearchTerm,
                query.IsActive,
                query.Phone,
                query.Email,
                query.Telegram);

            List<ClientDto> result = clients.Adapt<List<ClientDto>>();
            return result;
        }
    }
}