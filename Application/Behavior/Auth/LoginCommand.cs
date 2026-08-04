using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Behavior.Auth
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    public class LoginCommandHandler(
        IUserRepository userRepository,
        IJwtService jwtService) : IRequestHandler<LoginCommand, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            UserEntity? user = await userRepository.GetByEmailAsync(command.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Неверный email или пароль");

            if (string.IsNullOrEmpty(user.PasswordHash))
                throw new UnauthorizedAccessException("У пользователя не установлен пароль");

            bool passwordValid = jwtService.VerifyPassword(command.Password, user.PasswordHash);
            if (!passwordValid)
                throw new UnauthorizedAccessException("Неверный email или пароль");

            if (user.Status != UserStatus.Active)
                throw new UnauthorizedAccessException("Пользователь не активен");

            user.LastLoginAt = DateTime.UtcNow;
            await userRepository.UpdateAsync(user);

            string token = jwtService.GenerateToken(user);

            LoginResponse response = new LoginResponse
            {
                Token = token,
                UserId = user.Id,
                Name = user.Name,
                Role = user.Role.ToString()
            };

            return response;
        }
    }
}