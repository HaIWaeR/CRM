using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behavior.Branch
{
    public class GetBranchByIdQuery : IRequest<BranchEntity?>
    {
        public Guid Id { get; set; }
    }
    public class GetBranchByIdQueryHandler(IBranchRepository repository) : IRequestHandler<GetBranchByIdQuery, BranchEntity?>
    {
        public async Task<BranchEntity?> Handle(GetBranchByIdQuery query, CancellationToken cancellationToken)
        {
            BranchEntity? branch = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Филиал с ID {query.Id} не найден");

            return branch;
        }
    }

}
