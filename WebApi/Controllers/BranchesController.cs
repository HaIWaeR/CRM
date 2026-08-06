using Application.Behavior.Branch;
using Domain.Enums;
using Domain.Settings;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.DTOs.Branch;
using Shared.DTOs.Pagination;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController(IMediator mediator, IOptions<PaginationSettings> paginationOptions) : ControllerBase
    {
        private readonly PaginationSettings _paginationSettings = paginationOptions.Value;

        [Authorize(Roles = "Owner, Admin")]
        [HttpPost]
        public async Task<Guid> CreateBranchAsync([FromBody] BranchToCreateOrUpdateDto dto)
        {
            CreateBranchCommand command = dto.Adapt<CreateBranchCommand>();
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<PaginatedResult<BranchDto>> GetAllBranchesAsync(
            [FromQuery] int page = 1,
            [FromQuery] string? searchTerm = null,
            [FromQuery] BranchStatus? status = null)
        {
            int size = _paginationSettings.EntitySizes.Branches;

            GetAllBranchesQuery query = new GetAllBranchesQuery
            {
                Page = page,
                Size = size,
                SearchTerm = searchTerm,
                Status = status
            };

            return await mediator.Send(query);
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
