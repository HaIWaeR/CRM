using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Material
{
    public class ChangeMaterialStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public MaterialStatus Status { get; set; }
    }
    public class ChangeMaterialStatusCommandHandler(IMaterialRepository repository) : IRequestHandler<ChangeMaterialStatusCommand, bool>
    {
        public async Task<bool> Handle(ChangeMaterialStatusCommand command, CancellationToken cancellationToken)
        {
            MaterialEntity? material = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Материал с ID {command.Id} не найден");

            material.Status = command.Status;
            material.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(material);
            return true;
        }
    }
}
