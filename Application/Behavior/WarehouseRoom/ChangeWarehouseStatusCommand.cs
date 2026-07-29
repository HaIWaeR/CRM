using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.WarehouseRoom
{
    public class ChangeWarehouseStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public WarehouseStatus Status { get; set; }
    }

    public class ChangeWarehouseStatusCommandHandler(IWarehouseRoomRepository repository) : IRequestHandler<ChangeWarehouseStatusCommand, bool>
    {
        public async Task<bool> Handle(ChangeWarehouseStatusCommand command, CancellationToken cancellationToken)
        {
            WarehouseRoomEntity? warehouse = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Склад с ID {command.Id} не найден");

            warehouse.Status = command.Status;
            warehouse.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(warehouse);
            return true;
        }
    }
}