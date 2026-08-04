using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Behavior.Auth
{
    public class RegisterCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public Guid? BranchId { get; set; }
    }

    public class RegisterCommandHandler(
        IUserRepository userRepository,
        IJwtService jwtService) : IRequestHandler<RegisterCommand, Guid>
    {
        public async Task<Guid> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(command.Email))
            {
                bool emailUnique = await userRepository.IsEmailUniqueAsync(command.Email);
                if (!emailUnique)
                    throw new InvalidOperationException($"Пользователь с Email '{command.Email}' уже существует");
            }

            if (!string.IsNullOrWhiteSpace(command.Phone))
            {
                bool phoneUnique = await userRepository.IsPhoneUniqueAsync(command.Phone);
                if (!phoneUnique)
                    throw new InvalidOperationException($"Пользователь с телефоном '{command.Phone}' уже существует");
            }

            UserEntity user = command.Adapt<UserEntity>();
            user.Id = Guid.NewGuid();
            user.Role = UserRole.Reader;
            user.Status = UserStatus.Active;
            user.PasswordHash = jwtService.HashPassword(command.Password);
            user.CreatedAt = DateTime.UtcNow;

            await userRepository.AddAsync(user);
            return user.Id;
        }
    }
}