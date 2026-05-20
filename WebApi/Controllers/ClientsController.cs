using Application.Behavior.Client;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.Client;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateClientAsync([FromBody] CreateClientCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<ClientDto>> GetAllClientsAsync()
        {
            List<ClientEntity> clients = await mediator.Send(new GetAllClientsQuery());
            return clients.Adapt<List<ClientDto>>();
        }

        [HttpGet("{id}")]
        public async Task<ClientDto?> GetClientByIdAsync(Guid id)
        {
            ClientEntity? client = await mediator.Send(new GetClientByIdQuery { Id = id });
            return client.Adapt<ClientDto>();
        }

        [HttpPut("{id}")]
        public async Task<ClientDto?> UpdateClientAsync(Guid id, [FromBody] UpdateClientCommand command)
        {
            command.Id = id;
            ClientEntity? update = await mediator.Send(command);
            return update?.Adapt<ClientDto>();
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteClientAsync(Guid id)
        {
            return await mediator.Send(new DeleteClientCommand { Id = id });
        }
    }
}
