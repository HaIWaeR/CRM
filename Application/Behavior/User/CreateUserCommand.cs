using Application.Helpers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Behavior.User
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public Guid? BranchId { get; set; }
    }

    public class CreateUserCommandHandler(IUserRepository repository) : IRequestHandler<CreateUserCommand, Guid>
    {
        public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(command.Email))
            {
                if (!await repository.IsEmailUniqueAsync(command.Email))
                    throw new InvalidOperationException($"Пользователь с Email '{command.Email}' уже существует");
            }

            if (!string.IsNullOrWhiteSpace(command.Phone))
            {
                if (!await repository.IsPhoneUniqueAsync(command.Phone))
                    throw new InvalidOperationException($"Пользователь с телефоном '{command.Phone}' уже существует");
            }

            UserEntity user = command.Adapt<UserEntity>();
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;
            user.Role = UserRole.Reader;
            user.Status = UserStatus.Active;
            user.Phone = PhoneHelper.FormatPhone(command.Phone);

            await repository.AddAsync(user);
            return user.Id;
        }
    }
}