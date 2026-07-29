using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Behavior.Supplier
{
    public class DeleteSupplierCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteSupplierCommandHandler(ISupplierRepository repository) : IRequestHandler<DeleteSupplierCommand, bool>
    {
        public async Task<bool> Handle(DeleteSupplierCommand command, CancellationToken cancellationToken)
        {
            if (!await repository.ExistsAsync(command.Id))
                throw new KeyNotFoundException($"Поставщик с ID {command.Id} не найден");

            if (await repository.HasSupplierMaterialsAsync(command.Id))
                throw new InvalidOperationException("Нельзя удалить поставщика, так как у него есть привязанные материалы!");

            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}