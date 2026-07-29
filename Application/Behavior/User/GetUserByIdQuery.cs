using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.User;

namespace Application.Behavior.User
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public Guid Id { get; set; }
    }

    public class GetUserByIdQueryHandler(IUserRepository repository) : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            UserEntity? user = await repository.GetByIdAsync(query.Id)
                ?? throw new KeyNotFoundException($"Пользователь с ID {query.Id} не найден");

            UserDto result = user.Adapt<UserDto>();
            return result;
        }
    }
}