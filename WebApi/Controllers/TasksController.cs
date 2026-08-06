using Application.Behavior.Task;
using Domain.Enums;
using Domain.Settings;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.DTOs.Pagination;
using Shared.DTOs.Task;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController(IMediator mediator, IOptions<PaginationSettings> paginationOptions) : ControllerBase
    {
        private readonly PaginationSettings _paginationSettings = paginationOptions.Value;
        [Authorize(Roles = "Owner, Admin")]
        [HttpPost]
        public async Task<Guid> CreateTaskAsync([FromBody] TaskToCreateOrUpdateDto dto)
        {
            CreateTaskCommand command = dto.Adapt<CreateTaskCommand>();
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<PaginatedResult<TaskDto>> GetAllTasksAsync(
            [FromQuery] int page = 1,
            [FromQuery] string? searchTerm = null,
            [FromQuery] Guid? userId = null,
            [FromQuery] Guid? clientId = null,
            [FromQuery] Guid? orderId = null,
            [FromQuery] TaskPriority? priority = null,
            [FromQuery] InstallTaskStatus? status = null,
            [FromQuery] DateTime? fromDeadline = null,
            [FromQuery] DateTime? toDeadline = null)
        {
            int size = _paginationSettings.EntitySizes.Tasks;

            GetAllTasksQuery query = new GetAllTasksQuery
            {
                Page = page,
                Size = size,
                SearchTerm = searchTerm,
                UserId = userId,
                ClientId = clientId,
                OrderId = orderId,
                Priority = priority,
                Status = status,
                FromDeadline = fromDeadline,
                ToDeadline = toDeadline
            };

            return await mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<TaskDto> GetTaskByIdAsync(Guid id)
        {
            return await mediator.Send(new GetTaskByIdQuery { Id = id });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPut("{id}")]
        public async Task<TaskDto> UpdateTaskAsync(Guid id, [FromBody] TaskToCreateOrUpdateDto dto)
        {
            UpdateTaskCommand command = dto.Adapt<UpdateTaskCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            return await mediator.Send(new DeleteTaskCommand { Id = id });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPatch("{id}/status")]
        public async Task<bool> ChangeTaskStatusAsync(Guid id, [FromBody] InstallTaskStatus status)
        {
            return await mediator.Send(new ChangeTaskStatusCommand
            {
                Id = id,
                Status = status
            });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPatch("{id}/priority")]
        public async Task<bool> ChangeTaskPriorityAsync(Guid id, [FromBody] TaskPriority priority)
        {
            return await mediator.Send(new ChangeTaskPriorityCommand
            {
                Id = id,
                Priority = priority
            });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPatch("{id}/assign")]
        public async Task<bool> AssignTaskToUserAsync(Guid id, [FromBody] Guid userId)
        {
            return await mediator.Send(new AssignTaskToUserCommand
            {
                TaskId = id,
                UserId = userId
            });
        }
    }
}