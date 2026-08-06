using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.User;
using Shared.DTOs.Pagination;

namespace Application.Behavior.User
{
    public class GetAllUsersQuery : IRequest<PaginatedResult<UserDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public string? SearchTerm { get; set; }
        public UserRole? Role { get; set; }
        public UserStatus? Status { get; set; }
        public Guid? BranchId { get; set; }
    }

    public class GetAllUsersQueryHandler(IUserRepository repository) : IRequestHandler<GetAllUsersQuery, PaginatedResult<UserDto>>
    {
        public async Task<PaginatedResult<UserDto>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            List<UserEntity> users = await repository.GetFilteredAsync(
                query.SearchTerm,
                query.Role,
                query.Status,
                query.BranchId,
                query.Page,
                query.Size);

            int totalCount = await repository.GetTotalCountAsync(
                query.SearchTerm,
                query.Role,
                query.Status,
                query.BranchId);

            int totalPages = (int)Math.Ceiling(totalCount / (double)query.Size);

            if (query.Page > totalPages && totalPages > 0)
            {
                return new PaginatedResult<UserDto>
                {
                    Items = new List<UserDto>(),
                    TotalCount = totalCount,
                    Page = query.Page,
                    Size = query.Size,
                    TotalPages = totalPages
                };
            }

            List<UserDto> items = users.Adapt<List<UserDto>>();

            return new PaginatedResult<UserDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = query.Page,
                Size = query.Size,
                TotalPages = totalPages
            };
        }
    }
}