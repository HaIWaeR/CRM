using Application.DTO.Branch;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behavior.Branch
{
    public class GetAllBranchesQuery : IRequest<List<BranchDto>>;
    public class GetAllBranchesQueryHandler(IBranchRepository repository) : IRequestHandler<GetAllBranchesQuery, List<BranchDto>>
    {
        public async Task<List<BranchDto>> Handle(GetAllBranchesQuery query, CancellationToken cancellationToken)
        {
            List<BranchEntity> branches = await repository.GetAllAsync();

            return branches.Select(branch => new BranchDto
            {
                Id = branch.Id,
                Name = branch.Name,
                Address = branch.Address,
                Status = branch.Status,
                ContactPhone = branch.ContactPhone,
                ContactEmail = branch.ContactEmail,
                Description = branch.Description
            }).ToList();
        }
    }
}
