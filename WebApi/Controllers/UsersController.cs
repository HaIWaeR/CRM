using Application.Behavior.User;
using Domain.Enums;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.User;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [Authorize(Roles = "Owner, Admin")]
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
            [FromQuery] UserStatus? status = null,
            [FromQuery] Guid? branchId = null)
        {
            GetAllUsersQuery query = new GetAllUsersQuery
            {
                SearchTerm = searchTerm,
                Role = role,
                Status = status,
                BranchId = branchId
            };

            return await mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            return await mediator.Send(new GetUserByIdQuery { Id = id });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPut("{id}")]
        public async Task<UserDto> UpdateUserAsync(Guid id, [FromBody] UserToCreateOrUpdateDto dto)
        {
            UpdateUserCommand command = dto.Adapt<UpdateUserCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [Authorize(Roles = "Owner")]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            return await mediator.Send(new DeleteUserCommand { Id = id });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPatch("{id}/status")]
        public async Task<bool> ChangeUserStatusAsync(Guid id, [FromBody] UserStatus status)
        {
            return await mediator.Send(new ChangeUserStatusCommand
            {
                Id = id,
                Status = status
            });
        }

        [Authorize(Roles = "Owner")]
        [HttpPatch("{id}/role")]
        public async Task<bool> ChangeUserRoleAsync(Guid id, [FromBody] UserRole role)
        {
            return await mediator.Send(new ChangeUserRoleCommand
            {
                Id = id,
                Role = role
            });
        }
    }
}