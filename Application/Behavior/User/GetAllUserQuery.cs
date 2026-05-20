using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.User
{
    public class GetAllUsersQuery : IRequest<List<UserEntity>>;

    public class GetAllUsersQueryHandler(IUserRepository repository) : IRequestHandler<GetAllUsersQuery, List<UserEntity>>
    {
        public async Task<List<UserEntity>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            List<UserEntity> users = await repository.GetAllAsync();

            return users;
        }
    }
}