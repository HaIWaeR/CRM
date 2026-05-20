using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behavior.Branch
{
    public class GetAllBranchesQuery : IRequest<List<BranchEntity>>;
    public class GetAllBranchesQueryHandler(IBranchRepository repository) : IRequestHandler<GetAllBranchesQuery, List<BranchEntity>>
    {
        public async Task<List<BranchEntity>> Handle(GetAllBranchesQuery query, CancellationToken cancellationToken)
        {
            List<BranchEntity> branches = await repository.GetAllAsync();

            return branches;
        }
    }
}
  