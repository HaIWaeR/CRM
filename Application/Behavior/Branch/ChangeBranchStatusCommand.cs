using Application.Interfaces.Repositories;
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
            bool updated = await repository.ChangeBranchStatusAsync(command.Id, command.Status);

            if (!updated)
                throw new KeyNotFoundException($"Филиал с ID {command.Id} не найден");

            return true;
        }
    }
}