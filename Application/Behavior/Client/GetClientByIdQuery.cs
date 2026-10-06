using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.Client;

namespace Application.Behavior.Client;

/// <summary>
/// Запрос на получение одного клиента по идентификатору.
/// </summary>
public class GetClientByIdQuery : IRequest<ClientDto>
{
    public Guid Id { get; set; }
}
/// <summary>
/// Обработчик <see cref="GetClientByIdQuery"/>.
/// </summary>
/// <param name="repository">Репозиторий клиентов.</param>
public class GetClientByIdQueryHandler(IClientRepository repository) : IRequestHandler<GetClientByIdQuery, ClientDto>
{
    /// <summary>
    /// Загружает клиента из БД и преобразует его в <see cref="ClientDto"/>.
    /// </summary>
    /// <param name="query">Id искомого клиента.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Данные найденного клиента.</returns>
    /// <exception cref="KeyNotFoundException">Клиент с указанным Id не найден.</exception>
    public async Task<ClientDto> Handle(GetClientByIdQuery query, CancellationToken cancellationToken)
    {

        ClientEntity? client = await repository.GetByIdAsync(query.Id) 
            ?? throw new KeyNotFoundException($"Клиент с ID {query.Id} не найден");

        ClientDto result = client.Adapt<ClientDto>();
        return result;
    }
}