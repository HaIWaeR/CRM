using Application.Behavior.Material;
using Domain.Enums;
using Domain.Settings;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.DTOs.Material;
using Shared.DTOs.Pagination;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialsController(IMediator mediator, IOptions<PaginationSettings> paginationOptions) : ControllerBase
    {
        private readonly PaginationSettings _paginationSettings = paginationOptions.Value;
        [Authorize(Roles = "Owner, Admin")]
        [HttpPost]
        public async Task<Guid> CreateMaterialAsync([FromBody] MaterialToCreateOrUpdateDto dto)
        {
            CreateMaterialCommand command = dto.Adapt<CreateMaterialCommand>();
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<PaginatedResult<MaterialDto>> GetAllMaterialsAsync(
            [FromQuery] int page = 1,
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? categoryCode = null,
            [FromQuery] MaterialStatus? status = null,
            [FromQuery] string? article = null)
        {
            int size = _paginationSettings.EntitySizes.Materials;

            GetAllMaterialsQuery query = new GetAllMaterialsQuery
            {
                Page = page,
                Size = size,
                SearchTerm = searchTerm,
                CategoryCode = categoryCode,
                Status = status,
                Article = article
            };

            return await mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<MaterialDto> GetMaterialByIdAsync(Guid id)
        {
            
            return await mediator.Send(new GetMaterialByIdQuery { Id = id });

        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPut("{id}")]
        public async Task<MaterialDto> UpdateMaterialAsync(Guid id, [FromBody] MaterialToCreateOrUpdateDto dto)
        {
            UpdateMaterialCommand command = dto.Adapt<UpdateMaterialCommand>();
            command.Id = id;
            return await mediator.Send(command);
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteMaterialAsync(Guid id)
        {
            return await mediator.Send(new DeleteMaterialCommand { Id = id });
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPatch("{id}/status")]
        public async Task<bool> ChangeMaterialStatusAsync(Guid id, [FromBody] MaterialStatus status)
        {
            return await mediator.Send(new ChangeMaterialStatusCommand
            {
                Id = id,
                Status = status
            });
        }
    }
}