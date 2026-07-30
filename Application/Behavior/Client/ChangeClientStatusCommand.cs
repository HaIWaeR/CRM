using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Client
{
    public class ChangeClientStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public ClientStatus Status { get; set; }
    }

    public class ChangeClientStatusCommandHandler(IClientRepository repository) : IRequestHandler<ChangeClientStatusCommand, bool>
    {
        public async Task<bool> Handle(ChangeClientStatusCommand command, CancellationToken cancellationToken)
        {
            ClientEntity? client = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Клиент с ID {command.Id} не найден");

            client.Status = command.Status;
            client.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(client);
            return true;
        }
    }
}