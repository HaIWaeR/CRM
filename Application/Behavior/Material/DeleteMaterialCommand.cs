using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Behavior.Material
{
    public class DeleteMaterialCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteMaterialCommandHandler(IMaterialRepository repository) : IRequestHandler<DeleteMaterialCommand, bool>
    {
        public async Task<bool> Handle(DeleteMaterialCommand command, CancellationToken cancellationToken)
        {
            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}
