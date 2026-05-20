using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.Task;
using Application.Behavior.Task;
using Domain.Entities;
using Mapster;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateTaskAsync([FromBody] CreateTaskCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<TaskDto>> GetAllTasksAsync()
        {
            List<TaskEntity> tasks = await mediator.Send(new GetAllTasksQuery());
            return tasks.Adapt<List<TaskDto>>();
        }

        [HttpGet("{id}")]
        public async Task<TaskDto?> GetTaskByIdAsync(Guid id)
        {
            TaskEntity? task = await mediator.Send(new GetTaskByIdQuery { Id = id });
            return task.Adapt<TaskDto>();
        }

        [HttpPut("{id}")]
        public async Task<TaskDto?> UpdateTaskAsync(Guid id, [FromBody] UpdateTaskCommand command)
        {
            command.Id = id;
            TaskEntity? update = await mediator.Send(command);
            return update.Adapt<TaskDto>();
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            return await mediator.Send(new DeleteTaskCommand { Id = id });
        }
    }
}