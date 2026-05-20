using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.User
{
    public class GetUserByIdQuery : IRequest<UserEntity?>
    {
        public Guid Id { get; set; }
    }

    public class GetUserByIdQueryHandler(IUserRepository repository) : IRequestHandler<GetUserByIdQuery, UserEntity?>
    {
        public async Task<UserEntity?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            UserEntity? user = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Пользователь с ID {query.Id} не найден");

            return user;
        }
    }
}