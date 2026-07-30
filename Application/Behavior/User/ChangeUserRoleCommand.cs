using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.User
{
    public class ChangeUserRoleCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public UserRole Role { get; set; }
    }

    public class ChangeUserRoleCommandHandler(IUserRepository repository) : IRequestHandler<ChangeUserRoleCommand, bool>
    {
        public async Task<bool> Handle(ChangeUserRoleCommand command, CancellationToken cancellationToken)
        {
            UserEntity? user = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Пользователь с ID {command.Id} не найден");

            user.Role = command.Role;
            user.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(user);
            return true;
        }
    }
}