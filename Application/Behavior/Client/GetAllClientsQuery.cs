using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.Client;
using Shared.DTOs.Pagination;

namespace Application.Behavior.Client
{
    /// <summary>
    /// Запрос на получение страницы клиентов с фильтрацией и поиском.
    /// </summary>
    /// <remarks>
    /// Правила фильтров описаны в <see cref="IClientRepository.GetFilteredAsync"/>.
    /// </remarks>
    public class GetAllClientsQuery : IRequest<PaginatedResult<ClientDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public string? SearchTerm { get; set; }
        public ClientStatus? Status { get; set; }
        /// <summary>
        /// Фильтр по точному совпадению телефона; <c>null</c> — без фильтра.
        /// </summary>
        public string? Phone { get; set; }
        /// <summary>
        /// Фильтр по точному совпадению Email без учёта регистра; <c>null</c> — без фильтра.
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Фильтр по части имени Telegram; <c>null</c> — без фильтра.
        /// </summary>
        public string? Telegram { get; set; }
    }
    /// <summary>
    /// Обработчик <see cref="GetAllClientsQuery"/>.
    /// </summary>
    /// <param name="repository">Репозиторий клиентов.</param>
    public class GetAllClientsQueryHandler(IClientRepository repository) : IRequestHandler<GetAllClientsQuery, PaginatedResult<ClientDto>>
    {
        /// <summary>
        /// Загружает запрошенную страницу клиентов и считает общее количество подходящих записей.
        /// </summary>
        /// <param name="query">Номер страницы, её размер и фильтры.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>
        /// Страница клиентов с общим количеством записей и страниц.
        /// Если номер страницы больше их общего числа, <c>Items</c> будет пустым,
        /// но <c>TotalCount</c> и <c>TotalPages</c> заполнятся.
        /// </returns>
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