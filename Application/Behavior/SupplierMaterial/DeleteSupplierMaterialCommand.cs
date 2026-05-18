using MediatR;
using Application.Interfaces.Repositories;

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
            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}