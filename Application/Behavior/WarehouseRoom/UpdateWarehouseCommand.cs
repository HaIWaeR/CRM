using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.WarehouseRoom
{
    public class UpdateWarehouseRoomCommand : IRequest<WarehouseRoomEntity?>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPhone { get; set; }
        public string? Description { get; set; }
        public Guid? BranchId { get; set; }
    }

    public class UpdateWarehouseRoomCommandHandler(IWarehouseRoomRepository repository) : IRequestHandler<UpdateWarehouseRoomCommand, WarehouseRoomEntity?>
    {
        public async Task<WarehouseRoomEntity?> Handle(UpdateWarehouseRoomCommand command, CancellationToken cancellationToken)
        {
            WarehouseRoomEntity? warehouse = await repository.GetByIdAsync(command.Id) ?? throw new Exception($"Склад с ID {command.Id} не найден");
            warehouse.Name = command.Name;
            warehouse.Address = command.Address;
            warehouse.IsActive = command.IsActive;
            warehouse.ContactPerson = command.ContactPerson;
            warehouse.ContactPhone = command.ContactPhone;
            warehouse.Description = command.Description;
            warehouse.BranchId = command.BranchId;
            warehouse.UpdatedAt = DateTime.UtcNow;
            await repository.UpdateAsync(warehouse);

            return warehouse;
        }
    }
}