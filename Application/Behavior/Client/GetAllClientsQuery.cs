using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.Client;
using Shared.DTOs.Pagination;

namespace Application.Behavior.Client
{
    public class GetAllClientsQuery : IRequest<PaginatedResult<ClientDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public string? SearchTerm { get; set; }
        public ClientStatus? Status { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Telegram { get; set; }
    }
    public class GetAllClientsQueryHandler(IClientRepository repository) : IRequestHandler<GetAllClientsQuery, PaginatedResult<ClientDto>>
    {
        public async Task<PaginatedResult<ClientDto>> Handle(GetAllClientsQuery query, CancellationToken cancellationToken)
        {
            List<ClientEntity> clients = await repository.GetFilteredAsync(
                query.SearchTerm,
                query.Status,
                query.Phone,
                query.Email,
                query.Telegram,
                query.Page,
                query.Size);

            int totalCount = await repository.GetTotalCountAsync(
                query.SearchTerm,
                query.Status,
                query.Phone,
                query.Email,
                query.Telegram);

            int totalPages = (int)Math.Ceiling(totalCount / (double)query.Size);

            if (query.Page > totalPages && totalPages > 0)
            {
                return new PaginatedResult<ClientDto>
                {
                    Items = new List<ClientDto>(),
                    TotalCount = totalCount,
                    Page = query.Page,
                    Size = query.Size,
                    TotalPages = totalPages
                };
            }

            List<ClientDto> items = clients.Adapt<List<ClientDto>>();

            return new PaginatedResult<ClientDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = query.Page,
                Size = query.Size,
                TotalPages = totalPages
            };
        }
    }
}