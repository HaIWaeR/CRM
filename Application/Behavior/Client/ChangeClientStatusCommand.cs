using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behavior.Client
{
    public class ChangeClientStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }

    public class ChangeClientStatusCommandHandler(IClientRepository repository) : IRequestHandler<ChangeClientStatusCommand, bool>
    {
        public async Task<bool> Handle(ChangeClientStatusCommand command, CancellationToken cancellationToken)
        {
            ClientEntity? existing = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Клиент с ID {command.Id} не найден");

            existing.IsActive = command.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(existing);
            return true;
        }
    }
}