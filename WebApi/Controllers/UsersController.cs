using Application.Behavior.User;
using Domain.Enums;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.User;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateUserAsync([FromBody] UserToCreateOrUpdateDto dto)
        {
            CreateUserCommand command = dto.Adapt<CreateUserCommand>();
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<UserDto>> GetAllUsersAsync(
            [FromQuery] string? searchTerm = null,
            [FromQuery] UserRole? role = null,
            [FromQuery] bool? isActive = null,
            [FromQuery] Guid? branchId = null)
        {
            GetAllUsersQuery query = new GetAllUsersQuery
            {
                SearchTerm = searchTerm,
                Role = role,
                IsActive = isActive,
                BranchId = branchId
            };

            return await mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            return await mediator.Send(new GetUserByIdQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<UserDto> UpdateUserAsync(Guid id, [FromBody] UserToCreateOrUpdateDto dto)
        {
            UpdateUserCommand command = dto.Adapt<UpdateUserCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            return await mediator.Send(new DeleteUserCommand { Id = id });
        }

        [HttpPatch("{id}/status")]
        public async Task<bool> ChangeUserStatusAsync(Guid id, [FromBody] bool isActive)
        {
            return await mediator.Send(new ChangeUserStatusCommand
            {
                Id = id,
                IsActive = isActive
            });
        }
    }
}