using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.User;

namespace Application.Behavior.User
{
    public class GetAllUsersQuery : IRequest<List<UserDto>>
    {
        public string? SearchTerm { get; set; }
        public UserRole? Role { get; set; }
        public UserStatus? Status { get; set; }
        public Guid? BranchId { get; set; }
    }

    public class GetAllUsersQueryHandler(IUserRepository repository) : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        public async Task<List<UserDto>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            List<UserEntity> users = await repository.GetFilteredAsync(
                query.SearchTerm,
                query.Role,
                query.Status,
                query.BranchId);

            List<UserDto> result = users.Adapt<List<UserDto>>();
            return result;
        }
    }
}