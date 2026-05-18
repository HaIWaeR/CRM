using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DTO;
using Application.Behavior.Task;

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
            return await mediator.Send(new GetAllTasksQuery());
        }

        [HttpGet("{id}")]
        public async Task<TaskDto?> GetTaskByIdAsync(Guid id)
        {
            return await mediator.Send(new GetTaskByIdQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<TaskDto?> UpdateTaskAsync(Guid id, [FromBody] UpdateTaskCommand command)
        {
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            return await mediator.Send(new DeleteTaskCommand { Id = id });
        }
    }
}