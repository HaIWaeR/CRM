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
            if (string.IsNullOrWhiteSpace(command.Email))
                throw new Exception("Email обязателен");

            UserEntity? existingByEmail = await repository.GetByEmailAsync(command.Email);
            if (existingByEmail != null && existingByEmail.Id != command.Id)
                throw new Exception($"Пользователь с email '{command.Email}' уже существует");

            if (command.BranchId.HasValue)
            {
                bool branchExists = await repository.GetBranchByIdAsync(command.BranchId.Value);
                if (!branchExists)
                    throw new Exception($"Филиал с ID {command.BranchId} не существует");
            }

            UserEntity? user = await repository.GetByIdAsync(command.Id);
            if (user == null)
                throw new Exception($"Пользователь с ID {command.Id} не найден");

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