using Application.Behavior.Branch;
using Application.DTO.Branch;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateBranchAsync([FromBody] CreateBranchCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<BranchDto>> GetAllBranchesAsync()
        {
            return await mediator.Send(new GetAllBranchesQuery());
        }

        [HttpGet("{id}")]
        public async Task<BranchDto?> GetBranchByIdAsync(Guid id)
        {
            return await mediator.Send(new GetBranchByIdQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<BranchDto?> UpdateBranchAsync(Guid id, [FromBody] UpdateBranchCommand command)
        {
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteBranchAsync(Guid id)
        {
            return await mediator.Send(new DeleteBranchCommand { Id = id });
        }
    }
}
