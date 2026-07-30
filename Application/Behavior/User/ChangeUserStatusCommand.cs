using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.User
{
    public class ChangeUserStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public UserStatus Status { get; set; }
    }

    public class ChangeUserStatusCommandHandler(IUserRepository repository) : IRequestHandler<ChangeUserStatusCommand, bool>
    {
        public async Task<bool> Handle(ChangeUserStatusCommand command, CancellationToken cancellationToken)
        {
            UserEntity? user = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Пользователь с ID {command.Id} не найден");

            user.Status = command.Status;
            user.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(user);
            return true;
        }
    }
}