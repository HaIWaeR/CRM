using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behavior.Material
{
    public class ChangeMaterialStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
    public class ChangeMaterialStatusCommandHandler(IMaterialRepository repository) : IRequestHandler<ChangeMaterialStatusCommand, bool>
    {
        public async Task<bool> Handle(ChangeMaterialStatusCommand command, CancellationToken cancellationToken)
        {
            MaterialEntity? material = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Материал с ID {command.Id} не найден");

            material.IsActive = command.IsActive;
            material.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(material);
            return true;
        }
    }
}
