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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController(IMediator mediator, IOptions<PaginationSettings> paginationOptions) : ControllerBase
    {
        private readonly PaginationSettings _paginationSettings = paginationOptions.Value;

        [Authorize(Roles = "Owner, Admin")]
        [HttpPost]
        public async Task<Guid> CreateClientAsync([FromBody] ClientToCreateOrUpdateDto dto)
        {
            CreateClientCommand command = dto.Adapt<CreateClientCommand>();
            return await mediator.Send(command);
        }

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

        [HttpGet("{id}")]
        public async Task<ClientDto> GetClientByIdAsync(Guid id)
        {
            return await mediator.Send(new GetClientByIdQuery { Id = id });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPut("{id}")]
        public async Task<ClientDto> UpdateClientAsync(Guid id, [FromBody] ClientToCreateOrUpdateDto dto)
        {
            UpdateClientCommand command = dto.Adapt<UpdateClientCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteClientAsync(Guid id)
        {
            return await mediator.Send(new DeleteClientCommand { Id = id });
        }

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