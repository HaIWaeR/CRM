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