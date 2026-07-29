using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Behavior.User
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHandler(IUserRepository repository) : IRequestHandler<DeleteUserCommand, bool>
    {
        public async Task<bool> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            if (!await repository.ExistsAsync(command.Id))
                throw new KeyNotFoundException($"Пользователь с ID {command.Id} не найден");

            await repository.DeleteAsync(command.Id);
            return true;
        }
    }
}