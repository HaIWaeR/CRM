using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.User
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public Guid? BranchId { get; set; }
    }

    public class CreateUserCommandHandler(IUserRepository repository) : IRequestHandler<CreateUserCommand, Guid>
    {
        public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.Email))
                throw new Exception("Email обязателен");

            UserEntity? existing = await repository.GetByEmailAsync(command.Email)
                ?? throw new Exception($"Пользователь с email '{command.Email}' уже существует");

            if (command.BranchId.HasValue)
            {
                bool branchExists = await repository.GetBranchByIdAsync(command.BranchId.Value);
                if (!branchExists)
                    throw new Exception($"Филиал с ID {command.BranchId} не существует");
            }

            UserEntity user = new UserEntity
            {   
                Id = Guid.NewGuid(),
                Name = command.Name,
                Role = command.Role,
                IsActive = command.IsActive,
                Email = command.Email,
                PasswordHash = command.PasswordHash,
                BranchId = command.BranchId,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(user);
            return user.Id;
        }
    }
}