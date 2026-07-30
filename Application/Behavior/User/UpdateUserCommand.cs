using Application.Helpers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.User;

namespace Application.Behavior.User
{
    public class UpdateUserCommand : IRequest<UserDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public Guid? BranchId { get; set; }
    }

    public class UpdateUserCommandHandler(IUserRepository repository) : IRequestHandler<UpdateUserCommand, UserDto>
    {
        public async Task<UserDto> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            UserEntity? existing = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Пользователь с ID {command.Id} не найден");

            if (!string.IsNullOrWhiteSpace(command.Email))
            {
                bool isUnique = await repository.IsEmailUniqueAsync(command.Email, command.Id);
                if (!isUnique)
                    throw new InvalidOperationException($"Пользователь с Email '{command.Email}' уже существует");
            }

            if (!string.IsNullOrWhiteSpace(command.Phone))
            {
                bool isUnique = await repository.IsPhoneUniqueAsync(command.Phone, command.Id);
                if (!isUnique)
                    throw new InvalidOperationException($"Пользователь с телефоном '{command.Phone}' уже существует");
            }

            command.Adapt(existing);
            existing.UpdatedAt = DateTime.UtcNow;
            existing.Phone = PhoneHelper.FormatPhone(command.Phone);

            await repository.UpdateAsync(existing);

            UserDto result = existing.Adapt<UserDto>();
            return result;
        }
    }
}