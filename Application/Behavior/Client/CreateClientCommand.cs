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
            if (string.IsNullOrWhiteSpace(command.Phone) &&
                string.IsNullOrWhiteSpace(command.Email) &&
                string.IsNullOrWhiteSpace(command.Telegram))
            {
                throw new Exception("Должен быть указан хотя бы один контакт: телефон, email или Telegram");
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
