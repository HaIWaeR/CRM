using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.Supplier
{
    public class CreateSupplierCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string? Inn { get; set; }
        public string? Kpp { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? BankDetails { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
    public class CreateSupplierCommandHandler(ISupplierRepository repository) : IRequestHandler<CreateSupplierCommand, Guid>
    {
        public async Task<Guid> Handle(CreateSupplierCommand command, CancellationToken cancellationToken)
        {
            SupplierEntity supplier = new SupplierEntity
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Inn = command.Inn,
                Kpp = command.Kpp,
                Address = command.Address,
                Phone = command.Phone,
                Email = command.Email,
                Website = command.Website,
                BankDetails = command.BankDetails,
                Description = command.Description,
                IsActive = command.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(supplier);
            return supplier.Id;
        }
    }
}