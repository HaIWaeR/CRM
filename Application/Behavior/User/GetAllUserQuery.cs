using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.User
{
    public class GetAllUsersQuery : IRequest<List<UserDto>>;

    public class GetAllUsersQueryHandler(IUserRepository repository) : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        public async Task<List<UserDto>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            List<UserEntity> users = await repository.GetAllAsync();
            return users.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Role = user.Role,
                IsActive = user.IsActive,
                Email = user.Email,
                BranchId = user.BranchId,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            }).ToList();
        }
    }
}