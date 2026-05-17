using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Behavior.Client;

public class DeleteClientCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

public class DeleteClientCommandHandler(IClientRepository repository) : IRequestHandler<DeleteClientCommand, bool>
{
    public async Task<bool> Handle(DeleteClientCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(command.Id);
        return true;
    }
}   