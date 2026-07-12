using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.Branch;
using System.Text.Json.Serialization;

namespace Application.Behavior.Branch
{
    public class UpdateBranchCommand : IRequest<BranchDto>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateBranchCommandHandler(IBranchRepository repository) : IRequestHandler<UpdateBranchCommand, BranchDto>
    {
        public async Task<BranchDto> Handle(UpdateBranchCommand command, CancellationToken cancellationToken)
        {
            BranchEntity? existing = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Филиал с ID {command.Id} не найден");

            command.Adapt(existing);
            existing.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(existing);

            BranchDto result = existing.Adapt<BranchDto>();
            return result;
        }
    }
}