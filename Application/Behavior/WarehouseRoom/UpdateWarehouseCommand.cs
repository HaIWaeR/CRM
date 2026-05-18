using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.WarehouseRoom
{
    public class UpdateWarehouseRoomCommand : IRequest<WarehouseRoomDto?>
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

    public class UpdateWarehouseRoomCommandHandler(IWarehouseRoomRepository repository) : IRequestHandler<UpdateWarehouseRoomCommand, WarehouseRoomDto?>
    {
        public async Task<WarehouseRoomDto?> Handle(UpdateWarehouseRoomCommand command, CancellationToken cancellationToken)
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
            return new WarehouseRoomDto
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Address = warehouse.Address,
                IsActive = warehouse.IsActive,
                ContactPerson = warehouse.ContactPerson,
                ContactPhone = warehouse.ContactPhone,
                Description = warehouse.Description,
                BranchId = warehouse.BranchId,
                CreatedAt = warehouse.CreatedAt,
                UpdatedAt = warehouse.UpdatedAt
            };
        }
    }
}