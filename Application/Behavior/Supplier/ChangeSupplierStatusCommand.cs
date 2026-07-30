using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Supplier
{
    public class ChangeSupplierStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public SupplierStatus Status { get; set; }
    }

    public class ChangeSupplierStatusCommandHandler(ISupplierRepository repository) : IRequestHandler<ChangeSupplierStatusCommand, bool>
    {
        public async Task<bool> Handle(ChangeSupplierStatusCommand command, CancellationToken cancellationToken)
        {
            SupplierEntity? supplier = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Поставщик с ID {command.Id} не найден");

            supplier.Status = command.Status;
            supplier.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(supplier);
            return true;
        }
    }
}