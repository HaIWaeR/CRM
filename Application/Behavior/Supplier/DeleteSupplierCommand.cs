using MediatR;
using Application.Interfaces.Repositories;

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
            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}