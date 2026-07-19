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
        if(!await repository.ExistsAsync(command.Id))
            throw new KeyNotFoundException($"Клиент с ID {command.Id} не найден");

        if (await repository.HasOrdersAsync(command.Id))
            throw new InvalidOperationException("Нельзя удалить клиента, у которого есть заказы");

        await repository.DeleteAsync(command.Id);
        return true;
    }
}   