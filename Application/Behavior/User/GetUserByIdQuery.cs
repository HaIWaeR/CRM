using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.User
{
    public class GetUserByIdQuery : IRequest<UserDto?>
    {
        public Guid Id { get; set; }
    }

    public class GetUserByIdQueryHandler(IUserRepository repository) : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        public async Task<UserDto?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            UserEntity? user = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Пользователь с ID {query.Id} не найден");
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Role = user.Role,
                IsActive = user.IsActive,
                Email = user.Email,
                BranchId = user.BranchId,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }
}