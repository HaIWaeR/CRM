using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.Supplier
{
    public class UpdateSupplierCommand : IRequest<SupplierEntity?>
    {
        public Guid Id { get; set; }
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

    public class UpdateSupplierCommandHandler(ISupplierRepository repository) : IRequestHandler<UpdateSupplierCommand, SupplierEntity?>
    {
        public async Task<SupplierEntity?> Handle(UpdateSupplierCommand command, CancellationToken cancellationToken)
        {
            SupplierEntity? supplier = await repository.GetByIdAsync(command.Id) ?? throw new Exception($"Поставщик с ID {command.Id} не найден");
            supplier.Name = command.Name;
            supplier.Inn = command.Inn;
            supplier.Kpp = command.Kpp;
            supplier.Address = command.Address;
            supplier.Phone = command.Phone;
            supplier.Email = command.Email;
            supplier.Website = command.Website;
            supplier.BankDetails = command.BankDetails;
            supplier.Description = command.Description;
            supplier.IsActive = command.IsActive;
            supplier.UpdatedAt = DateTime.UtcNow;
            await repository.UpdateAsync(supplier);

            return supplier;
        }
    }
}