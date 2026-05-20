using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.Branch;
using Application.Behavior.Branch;
using Domain.Entities;

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
            List<BranchEntity> branches = await mediator.Send(new GetAllBranchesQuery());
            return branches.Adapt<List<BranchDto>>();
        }

        [HttpGet("{id}")]
        public async Task<BranchDto?> GetBranchByIdAsync(Guid id)
        {
            BranchEntity? branch = await mediator.Send(new GetBranchByIdQuery { Id = id });
            return branch?.Adapt<BranchDto>();
        }

        [HttpPut("{id}")]
        public async Task<BranchDto?> UpdateBranchAsync(Guid id, [FromBody] UpdateBranchCommand command)
        {
            command.Id = id;
            BranchEntity? update = await mediator.Send(command);
            return update?.Adapt<BranchDto>();
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteBranchAsync(Guid id)
        {
            return await mediator.Send(new DeleteBranchCommand { Id = id });
        }
    }
}
