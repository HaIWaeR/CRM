using Application.Helpers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.WarehouseRoom;

namespace Application.Behavior.WarehouseRoom
{
    public class UpdateWarehouseRoomCommand : IRequest<WarehouseRoomDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? ContactPerson { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public string? Description { get; set; }
        public Guid? BranchId { get; set; }
    }

    public class UpdateWarehouseRoomCommandHandler(IWarehouseRoomRepository repository) : IRequestHandler<UpdateWarehouseRoomCommand, WarehouseRoomDto>
    {
        public async Task<WarehouseRoomDto> Handle(UpdateWarehouseRoomCommand command, CancellationToken cancellationToken)
        {
            WarehouseRoomEntity? existing = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Склад с ID {command.Id} не найден");

            command.Adapt(existing);
            existing.UpdatedAt = DateTime.UtcNow;
            existing.ContactPhone = PhoneHelper.FormatPhone(command.ContactPhone);

            await repository.UpdateAsync(existing);

            WarehouseRoomDto result = existing.Adapt<WarehouseRoomDto>();
            return result;
        }
    }
}