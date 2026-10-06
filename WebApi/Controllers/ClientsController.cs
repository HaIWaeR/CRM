using Application.Behavior.Client;
using Domain.Enums;
using Domain.Settings;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.DTOs.Client;
using Shared.DTOs.Pagination;

namespace WebApi.Controllers
{
    /// <summary>
    /// REST API для работы с клиентами: создание, чтение, обновление, удаление и смена статуса.
    /// </summary>
    /// <remarks>
    /// Все методы требуют JWT-авторизации. Изменяющие методы доступны только ролям Owner и Admin.
    /// Контроллер не содержит бизнес-логики — он преобразует запрос в команду MediatR и возвращает результат.
    /// </remarks>
    /// <param name="mediator">Отправляет команды и запросы соответствующим обработчикам.</param>
    /// <param name="paginationOptions">Настройки пагинации; размер страницы клиентов берётся отсюда.</param>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController(IMediator mediator, IOptions<PaginationSettings> paginationOptions) : ControllerBase
    {
        private readonly PaginationSettings _paginationSettings = paginationOptions.Value;
        /// <summary>
        /// Создаёт нового клиента. <c>POST api/clients</c>, роли Owner, Admin.
        /// </summary>
        /// <param name="dto">Данные нового клиента.</param>
        /// <returns>Идентификатор созданного клиента.</returns>

        [Authorize(Roles = "Owner, Admin")]
        [HttpPost]
        public async Task<Guid> CreateClientAsync([FromBody] ClientToCreateOrUpdateDto dto)
        {
            CreateClientCommand command = dto.Adapt<CreateClientCommand>();
            return await mediator.Send(command);
        }
        /// <summary>
        /// Возвращает страницу клиентов с фильтрацией. <c>GET api/clients</c>.
        /// Размер страницы задаётся в конфигурации, а не в запросе.
        /// </summary>
        /// <param name="page">Номер страницы, начиная с 1.</param>
        /// <param name="searchTerm">Поиск по имени, фамилии, отчеству, телефону и Email.</param>
        /// <param name="status">Фильтр по статусу.</param>
        /// <param name="phone">Фильтр по точному совпадению телефона.</param>
        /// <param name="email">Фильтр по точному совпадению Email.</param>
        /// <param name="telegram">Фильтр по части имени Telegram.</param>
        /// <returns>Страница клиентов с данными для пагинации.</returns>
        [HttpGet]
        public async Task<PaginatedResult<ClientDto>> GetAllClientsAsync(
            [FromQuery] int page = 1,
            [FromQuery] string? searchTerm = null,
            [FromQuery] ClientStatus? status = null,
            [FromQuery] string? phone = null,
            [FromQuery] string? email = null,
            [FromQuery] string? telegram = null)
        {
            int size = _paginationSettings.EntitySizes.Clients;

            GetAllClientsQuery query = new GetAllClientsQuery
            {
                Page = page,
                Size = size,
                SearchTerm = searchTerm,
                Status = status,
                Phone = phone,
                Email = email,
                Telegram = telegram
            };

            return await mediator.Send(query);
        }
        /// <summary>
        /// Возвращает клиента по идентификатору. <c>GET api/clients/{id}</c>.
        /// </summary>
        /// <param name="id">Идентификатор клиента.</param>
        /// <returns>Данные клиента.</returns>
        [HttpGet("{id}")]
        public async Task<ClientDto> GetClientByIdAsync(Guid id)
        {
            return await mediator.Send(new GetClientByIdQuery { Id = id });
        }
        /// <summary>
        /// Полностью обновляет данные клиента. <c>PUT api/clients/{id}</c>, роли Owner, Admin.
        /// </summary>
        /// <param name="id">Идентификатор изменяемого клиента.</param>
        /// <param name="dto">Новые данные клиента; незаполненные поля будут очищены.</param>
        /// <returns>Клиент после обновления.</returns>
        [Authorize(Roles = "Owner, Admin")]
        [HttpPut("{id}")]
        public async Task<ClientDto> UpdateClientAsync(Guid id, [FromBody] ClientToCreateOrUpdateDto dto)
        {
            UpdateClientCommand command = dto.Adapt<UpdateClientCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        /// <summary>
        /// Удаляет клиента без заказов. <c>DELETE api/clients/{id}</c>, роли Owner, Admin.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого клиента.</param>
        /// <returns><c>true</c>, если клиент удалён.</returns>
        [Authorize(Roles = "Owner, Admin")]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteClientAsync(Guid id)
        {
            return await mediator.Send(new DeleteClientCommand { Id = id });
        }
        /// <summary>
        /// Меняет статус клиента. <c>PATCH api/clients/{id}/status</c>, роли Owner, Admin.
        /// </summary>
        /// <param name="id">Идентификатор клиента.</param>
        /// <param name="status">Новый статус; в теле запроса передаётся числом (1, 2 или 3).</param>
        /// <returns><c>true</c>, если статус изменён.</returns>
        [Authorize(Roles = "Owner, Admin")]
        [HttpPatch("{id}/status")]
        public async Task<bool> ChangeClientStatusAsync(Guid id, [FromBody] ClientStatus status)
        {
            return await mediator.Send(new ChangeClientStatusCommand
            {
                Id = id,
                Status = status
            });
        }
    }
}