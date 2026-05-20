using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.User;
using Application.Behavior.User;
using Domain.Entities;
using Mapster;

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
            List<UserEntity> users = await mediator.Send(new GetAllUsersQuery());
            return users.Adapt<List<UserDto>>();
        }

        [HttpGet("{id}")]
        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            UserEntity? user = await mediator.Send(new GetUserByIdQuery { Id = id });
            return user.Adapt<UserDto>();
        }

        [HttpPut("{id}")]
        public async Task<UserDto?> UpdateUserAsync(Guid id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;
            UserEntity? update = await mediator.Send(command);
            return update.Adapt<UserDto>();
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            return await mediator.Send(new DeleteUserCommand { Id = id });
        }
    }
}