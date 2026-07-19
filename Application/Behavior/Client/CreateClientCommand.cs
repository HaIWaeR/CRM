using Application.Helpers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Behavior.Client
{
    public class CreateClientCommand : IRequest<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
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
            bool hasContact = !string.IsNullOrWhiteSpace(command.Phone) ||
                              !string.IsNullOrWhiteSpace(command.Email) ||
                              !string.IsNullOrWhiteSpace(command.Telegram);
            if (!hasContact)
                throw new InvalidOperationException("У клиента должен быть указан хотя бы один контакт (телефон, Email или Telegram)");

            if (!string.IsNullOrWhiteSpace(command.Phone))
            {
                bool phoneUnique = await repository.IsPhoneUniqueAsync(command.Phone);
                if (!phoneUnique)
                    throw new InvalidOperationException("Клиент с таким телефоном уже существует");
            }

            if (!string.IsNullOrWhiteSpace(command.Email))
            {
                bool emailUnique = await repository.IsEmailUniqueAsync(command.Email);
                if (!emailUnique)
                    throw new InvalidOperationException("Клиент с таким Email уже существует");
            }

            if (!string.IsNullOrWhiteSpace(command.Telegram))
            {
                bool telegramUnique = await repository.IsTelegramUniqueAsync(command.Telegram);
                if (!telegramUnique)
                    throw new InvalidOperationException("Клиент с таким Telegram уже существует");
            }

            ClientEntity client = command.Adapt<ClientEntity>();
            client.Phone = PhoneHelper.FormatPhone(command.Phone);
            client.Id = Guid.NewGuid();
            client.CreatedAt = DateTime.UtcNow;
            client.IsActive = true;

            await repository.AddAsync(client);
            return client.Id;
        }
    }
}
