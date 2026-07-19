using Application.Behavior.Branch;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Branch;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> CreateBranchAsync([FromBody] BranchToCreateOrUpdateDto dto)
        {
            CreateBranchCommand command = dto.Adapt<CreateBranchCommand>();
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<BranchDto>> GetAllBranchesAsync(
            [FromQuery] string? searchTerm = null,
            [FromQuery] Domain.Enums.BranchStatus? status = null)
        {
            GetAllBranchesQuery query = new GetAllBranchesQuery
            {
                SearchTerm = searchTerm,
                Status = status
            };

            List<BranchDto> result = await mediator.Send(query);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<BranchDto> GetBranchByIdAsync(Guid id)
        {
            return await mediator.Send(new GetBranchByIdQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<BranchDto> UpdateBranchAsync(Guid id, [FromBody] BranchToCreateOrUpdateDto dto)
        {
            UpdateBranchCommand command = dto.Adapt<UpdateBranchCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteBranchAsync(Guid id)
        {
            return await mediator.Send(new DeleteBranchCommand { Id = id });
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<bool> ChangeBranchStatusAsync(Guid id, [FromBody] Domain.Enums.BranchStatus status)
        {
            return await mediator.Send(new ChangeBranchStatusCommand
            {
                Id = id,
                Status = status
            });
        }
    }
}
