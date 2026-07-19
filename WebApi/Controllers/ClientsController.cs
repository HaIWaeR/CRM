using Application.Behavior.Client;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Client;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateClientAsync([FromBody] ClientToCreateOrUpdateDto dto)
        {
            CreateClientCommand command = dto.Adapt<CreateClientCommand>();
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<ClientDto>> GetAllClientsAsync(
            [FromQuery] string? searchTerm = null,
            [FromQuery] bool? isActive = null,
            [FromQuery] string? phone = null,
            [FromQuery] string? email = null,
            [FromQuery] string? telegram = null)
        {
            GetAllClientsQuery query = new GetAllClientsQuery
            {
                SearchTerm = searchTerm,
                IsActive = isActive,
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

        [HttpPut("{id}")]
        public async Task<ClientDto> UpdateClientAsync(Guid id, [FromBody] ClientToCreateOrUpdateDto dto)
        {
            UpdateClientCommand command = dto.Adapt<UpdateClientCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteClientAsync(Guid id)
        {
            return await mediator.Send(new DeleteClientCommand { Id = id });
        }

        [HttpPatch("{id}/status")]
        public async Task<bool> ChangeClientStatusAsync(Guid id, [FromBody] bool isActive)
        {
            return await mediator.Send(new ChangeClientStatusCommand
            {
                Id = id,
                IsActive = isActive
            });
        }
    }
}
