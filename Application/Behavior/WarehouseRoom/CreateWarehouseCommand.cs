using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.WarehouseRoom
{
    public class CreateWarehouseRoomCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPhone { get; set; }
        public string? Description { get; set; }
        public Guid? BranchId { get; set; }
    }

    public class CreateWarehouseRoomCommandHandler(IWarehouseRoomRepository repository) : IRequestHandler<CreateWarehouseRoomCommand, Guid>
    {
        public async Task<Guid> Handle(CreateWarehouseRoomCommand command, CancellationToken cancellationToken)
        {
            WarehouseRoomEntity warehouse = new WarehouseRoomEntity
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Address = command.Address,
                IsActive = command.IsActive,
                ContactPerson = command.ContactPerson,
                ContactPhone = command.ContactPhone,
                Description = command.Description,
                BranchId = command.BranchId,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(warehouse);
            return warehouse.Id;
        }
    }
}