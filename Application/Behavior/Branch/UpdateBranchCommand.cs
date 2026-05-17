using Application.DTO.Branch;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Branch
{
    public class UpdateBranchCommand : IRequest<BranchDto?>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public BranchStatus Status { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateBranchCommandHandler(IBranchRepository repository) : IRequestHandler<UpdateBranchCommand, BranchDto?>
    {
        public async Task<BranchDto?> Handle(UpdateBranchCommand command, CancellationToken cancellationToken)
        {
            BranchEntity? branch = await repository.GetByIdAsync(command.Id) ?? throw new Exception($"Филиал с ID {command.Id} не найден");

            branch.Name = command.Name;
            branch.Address = command.Address;
            branch.Status = command.Status;
            branch.ContactPhone = command.ContactPhone;
            branch.ContactEmail = command.ContactEmail;
            branch.Description = command.Description;
            branch.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(branch);

            return new BranchDto
            {
                Id = branch.Id,
                Name = branch.Name,
                Address = branch.Address,
                Status = branch.Status,
                ContactPhone = branch.ContactPhone,
                ContactEmail = branch.ContactEmail,
                Description = branch.Description
            };
        }
    }
}
