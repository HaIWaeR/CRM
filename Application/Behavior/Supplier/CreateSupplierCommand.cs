using Application.Helpers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Behavior.Supplier
{
    public class CreateSupplierCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string? Inn { get; set; }
        public string? Kpp { get; set; }
        public string? Address { get; set; }
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public Dictionary<string, string>? BankDetails { get; set; }
        public string? Description { get; set; }
        public int? Rating { get; set; }
        public SupplierType SupplierType { get; set; }
    }

    public class CreateSupplierCommandHandler(ISupplierRepository repository) : IRequestHandler<CreateSupplierCommand, Guid>
    {
        public async Task<Guid> Handle(CreateSupplierCommand command, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(command.Inn))
            {
                if (!await repository.IsInnUniqueAsync(command.Inn))
                    throw new InvalidOperationException($"Поставщик с ИНН '{command.Inn}' уже существует");
            }

            SupplierEntity supplier = command.Adapt<SupplierEntity>();
            supplier.Id = Guid.NewGuid();
            supplier.CreatedAt = DateTime.UtcNow;
            supplier.IsActive = true;
            supplier.Phone = PhoneHelper.FormatPhone(command.Phone);

            await repository.AddAsync(supplier);
            return supplier.Id;
        }
    }
}