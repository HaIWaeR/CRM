using Application.Helpers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Behavior.WarehouseRoom
{
    public class CreateWarehouseRoomCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? ContactPerson { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public string? Description { get; set; }
        public Guid? BranchId { get; set; }
    }

    public class CreateWarehouseRoomCommandHandler(IWarehouseRoomRepository repository) : IRequestHandler<CreateWarehouseRoomCommand, Guid>
    {
        public async Task<Guid> Handle(CreateWarehouseRoomCommand command, CancellationToken cancellationToken)
        {
            WarehouseRoomEntity warehouse = command.Adapt<WarehouseRoomEntity>();
            warehouse.Id = Guid.NewGuid();
            warehouse.CreatedAt = DateTime.UtcNow;
            warehouse.Status = WarehouseStatus.Inactive;
            warehouse.ContactPhone = PhoneHelper.FormatPhone(command.ContactPhone);

            await repository.AddAsync(warehouse);
            return warehouse.Id;
        }
    }
}