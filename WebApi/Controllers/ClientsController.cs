using Application.Behavior.Client;
using Application.DTO.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            GetAllClientsQuery query = new GetAllClientsQuery();
            return await mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ClientDto?> GetClientByIdAsync(Guid id)
        {
            GetClientByIdQuery query = new GetClientByIdQuery { Id = id };

            return await mediator.Send(query);
        }

        [HttpPut("{id}")]
        public async Task<ClientDto?> UpdateClientAsync(Guid id, [FromBody] UpdateClientCommand command)
        {
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await mediator.Send(new DeleteClientCommand { Id = id });
        }
    }
}
