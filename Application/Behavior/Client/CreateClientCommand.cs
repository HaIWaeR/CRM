using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behavior.Client
{
    public class CreateClientCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Telegram { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
    }

    public class CreateClientCommandHandler(IClientRepository repository) : IRequestHandler<CreateClientCommand, Guid>
    {
        public async Task<Guid> Handle(CreateClientCommand command, CancellationToken cancellationToken)
        {
            ClientEntity? name = await repository.GetByNameAsync(command.Name);
            if (name != null)
                throw new Exception("Клиент с таким именем уже существует.");

            if (string.IsNullOrWhiteSpace(command.Phone) &&
                string.IsNullOrWhiteSpace(command.Email) &&
                string.IsNullOrWhiteSpace(command.Telegram))
            {
                throw new Exception("Необходимо указать хотя бы один контакт: телефон, email или telegram.");
            }

            if (!string.IsNullOrWhiteSpace(command.Email))
            {
                ClientEntity? email = await repository.GetByEmailAsync(command.Email);
                if (email != null)
                    throw new Exception("Клиент с таким email уже существует.");
            }

            if (!string.IsNullOrWhiteSpace(command.Phone))
            {
                ClientEntity? phone = await repository.GetByPhoneAsync(command.Phone);
                if (phone != null)
                    throw new Exception("Клиент с таким номером телефона уже существует.");
            }

            if (!string.IsNullOrWhiteSpace(command.Telegram))
            {
                ClientEntity? telegram = await repository.GetByTelegramAsync(command.Telegram);
                if (telegram != null)
                    throw new Exception("Клиент с таким Telegram уже существует.");
            }

            ClientEntity client = new()
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Phone = command.Phone,
                Email = command.Email,
                Telegram = command.Telegram,
                Address = command.Address,
                Notes = command.Notes,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(client);

            return client.Id;
        }
    }
}
