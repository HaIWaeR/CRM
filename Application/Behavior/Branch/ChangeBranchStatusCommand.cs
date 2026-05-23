using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Branch
{
    public class ChangeBranchStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public BranchStatus Status { get; set; }
    }

    public class ChangeBranchStatusCommandHandler(IBranchRepository repository) : IRequestHandler<ChangeBranchStatusCommand, bool>
    {
        public async Task<bool> Handle(ChangeBranchStatusCommand command, CancellationToken cancellationToken)
        {
            BranchEntity? existing = await repository.GetByIdAsync(command.Id);

            if (existing == null)
                throw new Exception($"Филиал с ID '{command.Id}' не найден");

            return await repository.ChangeBranchStatusAsync(command.Id, command.Status);
        }
    }
}
