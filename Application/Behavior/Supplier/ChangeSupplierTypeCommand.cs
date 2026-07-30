using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

public class ChangeSupplierTypeCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public SupplierType SupplierType { get; set; }
}

public class ChangeSupplierTypeCommandHandler(ISupplierRepository repository) : IRequestHandler<ChangeSupplierTypeCommand, bool>
{
    public async Task<bool> Handle(ChangeSupplierTypeCommand command, CancellationToken cancellationToken)
    {
        SupplierEntity? supplier = await repository.GetByIdAsync(command.Id)
            ?? throw new KeyNotFoundException($"Поставщик с ID {command.Id} не найден");

        supplier.Type = command.SupplierType;
        supplier.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(supplier);
        return true;
    }
}