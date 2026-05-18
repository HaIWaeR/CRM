using MediatR;
using Application.Interfaces.Repositories;

namespace Application.Behavior.Product
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProductCommandHandler(IProductRepository repository) : IRequestHandler<DeleteProductCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}