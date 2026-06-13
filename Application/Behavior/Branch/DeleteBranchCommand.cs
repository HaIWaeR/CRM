using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Behavior.Branch
{
    public class DeleteBranchCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteBranchCommandHandler(IBranchRepository repository) : IRequestHandler<DeleteBranchCommand, bool>
    {
        public async Task<bool> Handle(DeleteBranchCommand command, CancellationToken cancellationToken)
        {
            var branch = await repository.GetByIdAsync(command.Id)
                ?? throw new Exception($"Филиал с ID {command.Id} не найден");
                
            if (await repository.HasOrdersAsync(command.Id))
                throw new Exception("Нельзя удалить филиал, у которого есть заказы!");

            if (await repository.HasUsersAsync(command.Id))
                throw new Exception("Нельзя удалить филиал, к которому привязаны сотрудники!");

            if (await repository.HasWarehousesAsync(command.Id))
                throw new Exception("Нельзя удалить филиал, к которому привязаны склады!");

            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}
