using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.Branch;

namespace Application.Behavior.Branch
{
    public class GetAllBranchesQuery : IRequest<List<BranchDto>>
    {
        public string? SearchTerm { get; set; }
        public BranchStatus? Status { get; set; }
    }

    public class GetAllBranchesQueryHandler(IBranchRepository repository) : IRequestHandler<GetAllBranchesQuery, List<BranchDto>>
    {
        public async Task<List<BranchDto>> Handle(GetAllBranchesQuery query, CancellationToken cancellationToken)
        {
            List<BranchEntity> branches = await repository.GetFilteredAsync(query.SearchTerm, query.Status);
            List<BranchDto> result = branches.Adapt<List<BranchDto>>();
            return result;
        }
    }
}