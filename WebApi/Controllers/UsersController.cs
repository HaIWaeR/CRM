using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DTO;
using Application.Behavior.User;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateUserAsync([FromBody] CreateUserCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await mediator.Send(new GetAllUsersQuery());
        }

        [HttpGet("{id}")]
        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            return await mediator.Send(new GetUserByIdQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<UserDto?> UpdateUserAsync(Guid id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            return await mediator.Send(new DeleteUserCommand { Id = id });
        }
    }
}