using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Behavior.Client;

/// <summary>
/// Команда на удаление клиента. Возвращает <c>true</c> при успехе.
/// </summary>
/// <remarks>
/// Удаление физическое — запись исчезает из БД. Клиента с заказами удалить нельзя;
/// вместо этого его можно перевести в статус <see cref="Domain.Enums.ClientStatus.Inactive"/>.
/// </remarks>
public class DeleteClientCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

/// <summary>
/// Обработчик <see cref="DeleteClientCommand"/>.
/// </summary>
/// <param name="repository">Репозиторий клиентов.</param>
public class DeleteClientCommandHandler(IClientRepository repository) : IRequestHandler<DeleteClientCommand, bool>
{
    public async Task<bool> Handle(DeleteClientCommand command, CancellationToken cancellationToken)
    {
        if(!await repository.ExistsAsync(command.Id))
            throw new KeyNotFoundException($"Клиент с ID {command.Id} не найден");

        if (await repository.HasOrdersAsync(command.Id))
            throw new InvalidOperationException("Нельзя удалить клиента, у которого есть заказы");

        await repository.DeleteAsync(command.Id);
        return true;
    }
}   