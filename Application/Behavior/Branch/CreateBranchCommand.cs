using Application.Helpers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Behavior.Branch
{
    public class CreateBranchCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public string? Description { get; set; }
    }

    public class CreateBranchCommandHandler(IBranchRepository repository) : IRequestHandler<CreateBranchCommand, Guid>
    {
        public async Task<Guid> Handle(CreateBranchCommand command, CancellationToken cancellationToken)
        {
            BranchEntity branch = command.Adapt<BranchEntity>();
            branch.Id = Guid.NewGuid();
            branch.CreatedAt = DateTime.UtcNow;
            branch.Status = BranchStatus.Maintenance;
            branch.ContactPhone = PhoneHelper.FormatPhone(command.ContactPhone);

            await repository.AddAsync(branch);
            return branch.Id;
        }
    }
}