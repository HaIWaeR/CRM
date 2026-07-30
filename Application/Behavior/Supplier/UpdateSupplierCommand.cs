using Application.Helpers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.Supplier;

namespace Application.Behavior.Supplier
{
    public class UpdateSupplierCommand : IRequest<SupplierDto>
    {
        public Guid Id { get; set; }
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
    }

    public class UpdateSupplierCommandHandler(ISupplierRepository repository) : IRequestHandler<UpdateSupplierCommand, SupplierDto>
    {
        public async Task<SupplierDto> Handle(UpdateSupplierCommand command, CancellationToken cancellationToken)
        {
            SupplierEntity? existing = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Поставщик с ID {command.Id} не найден");

            if (!string.IsNullOrWhiteSpace(command.Inn))
            {
                bool isUnique = await repository.IsInnUniqueAsync(command.Inn, command.Id);
                if (!isUnique)
                    throw new InvalidOperationException($"Поставщик с ИНН '{command.Inn}' уже существует");
            }

            command.Adapt(existing);
            existing.UpdatedAt = DateTime.UtcNow;
            existing.Phone = PhoneHelper.FormatPhone(command.Phone);

            await repository.UpdateAsync(existing);

            SupplierDto result = existing.Adapt<SupplierDto>();
            return result;
        }
    }
}