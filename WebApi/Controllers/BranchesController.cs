using Application.Behavior.Branch;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Branch;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController(IMediator mediator) : ControllerBase
    {
        [Authorize(Roles = "Owner, Admin")]
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

        [Authorize(Roles = "Owner, Admin")]
        [HttpPut("{id}")]
        public async Task<BranchDto> UpdateBranchAsync(Guid id, [FromBody] BranchToCreateOrUpdateDto dto)
        {
            UpdateBranchCommand command = dto.Adapt<UpdateBranchCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [Authorize(Roles = "Owner")]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteBranchAsync(Guid id)
        {
            return await mediator.Send(new DeleteBranchCommand { Id = id });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPatch("{id}/status")]
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
