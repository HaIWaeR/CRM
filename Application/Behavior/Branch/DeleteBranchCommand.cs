using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Behavior.Branch
{
    public class DeleteBranchCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteBranchCommandHandler(IBranchRepository repository) : IRequestHandler<DeleteBranchCommand, bool>
    {
        public async Task<bool> Handle(DeleteBranchCommand command, CancellationToken cancellationToken)
        {
            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}
