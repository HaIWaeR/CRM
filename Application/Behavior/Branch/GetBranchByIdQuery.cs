using Application.DTO.Branch;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behavior.Branch
{
    public class GetBranchByIdQuery : IRequest<BranchDto?>
    {
        public Guid Id { get; set; }
    }
    public class GetBranchByIdQueryHandler(IBranchRepository repository) : IRequestHandler<GetBranchByIdQuery, BranchDto?>
    {
        public async Task<BranchDto?> Handle(GetBranchByIdQuery query, CancellationToken cancellationToken)
        {
            BranchEntity? branch = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Филиал с ID {query.Id} не найден");

            return new BranchDto
            {
                Id = branch.Id,
                Name = branch.Name,
                Address = branch.Address,
                Status = branch.Status,
                ContactPhone = branch.ContactPhone,
                ContactEmail = branch.ContactEmail,
                Description = branch.Description
            };
        }
    }

}
