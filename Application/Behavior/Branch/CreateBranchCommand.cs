using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Branch
{
    public class CreateBranchCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public BranchStatus? Status { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public string? Description { get; set; }
    }
    public class CreateBranchCommandHandler(IBranchRepository repository) : IRequestHandler<CreateBranchCommand, Guid>
    {
        public async Task<Guid> Handle(CreateBranchCommand command, CancellationToken cancellationToken)
        {
            BranchEntity? existing = await repository.GetByNameAsync(command.Name) 
                ?? throw new Exception($"Филиал с названием '{command.Name}' уже существует");

            BranchEntity branch = new BranchEntity
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Address = command.Address,
                Status = command.Status ?? BranchStatus.Maintenance,
                ContactPhone = command.ContactPhone,
                ContactEmail = command.ContactEmail,
                Description = command.Description,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(branch);

            return branch.Id;
        }
    }
}
