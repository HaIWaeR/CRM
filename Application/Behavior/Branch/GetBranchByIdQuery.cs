using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.Branch;

namespace Application.Behavior.Branch
{
    public class GetBranchByIdQuery : IRequest<BranchDto>
    {
        public Guid Id { get; set; }
    }

    public class GetBranchByIdQueryHandler(IBranchRepository repository) : IRequestHandler<GetBranchByIdQuery, BranchDto>
    {
        public async Task<BranchDto> Handle(GetBranchByIdQuery query, CancellationToken cancellationToken)
        {
            BranchEntity? branch = await repository.GetByIdAsync(query.Id)
                ?? throw new KeyNotFoundException($"Филиал с ID {query.Id} не найден");

            BranchDto result = branch.Adapt<BranchDto>();
            return result;
        }
    }
}