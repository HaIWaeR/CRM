using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Behavior.SupplierMaterial
{
    public class DeleteSupplierMaterialCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteSupplierMaterialCommandHandler(ISupplierMaterialRepository repository) : IRequestHandler<DeleteSupplierMaterialCommand, bool>
    {
        public async Task<bool> Handle(DeleteSupplierMaterialCommand command, CancellationToken cancellationToken)
        {
            if (!await repository.ExistsAsync(command.Id))
                throw new KeyNotFoundException($"Запись с ID {command.Id} не найдена");

            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}