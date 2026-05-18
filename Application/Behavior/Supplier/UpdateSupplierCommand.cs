using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.Supplier
{
    public class UpdateSupplierCommand : IRequest<SupplierDto?>
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

    public class UpdateSupplierCommandHandler(ISupplierRepository repository) : IRequestHandler<UpdateSupplierCommand, SupplierDto?>
    {
        public async Task<SupplierDto?> Handle(UpdateSupplierCommand command, CancellationToken cancellationToken)
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
            return new SupplierDto
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Inn = supplier.Inn,
                Kpp = supplier.Kpp,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                Website = supplier.Website,
                BankDetails = supplier.BankDetails,
                Description = supplier.Description,
                IsActive = supplier.IsActive,
                CreatedAt = supplier.CreatedAt,
                UpdatedAt = supplier.UpdatedAt
            };
        }
    }
}