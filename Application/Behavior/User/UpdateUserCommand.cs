using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.User
{
    public class UpdateUserCommand : IRequest<UserEntity?>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public Guid? BranchId { get; set; }
    }

    public class UpdateUserCommandHandler(IUserRepository repository) : IRequestHandler<UpdateUserCommand, UserEntity?>
    {
        public async Task<UserEntity?> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            UserEntity? user = await repository.GetByIdAsync(command.Id) ?? throw new Exception($"Пользователь с ID {command.Id} не найден");
            user.Name = command.Name;
            user.Role = command.Role;
            user.IsActive = command.IsActive;
            user.Email = command.Email;
            user.PasswordHash = command.PasswordHash;
            user.BranchId = command.BranchId;
            user.UpdatedAt = DateTime.UtcNow;
            await repository.UpdateAsync(user);

            return user;
        }
    }
}