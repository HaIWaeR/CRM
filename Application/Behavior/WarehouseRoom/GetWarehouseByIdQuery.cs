using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using Shared.DTOs.WarehouseRoom;

namespace Application.Behavior.WarehouseRoom
{
    public class GetWarehouseRoomByIdQuery : IRequest<WarehouseRoomDto>
    {
        public Guid Id { get; set; }
    }

    public class GetWarehouseRoomByIdQueryHandler(IWarehouseRoomRepository repository) : IRequestHandler<GetWarehouseRoomByIdQuery, WarehouseRoomDto>
    {
        public async Task<WarehouseRoomDto> Handle(GetWarehouseRoomByIdQuery query, CancellationToken cancellationToken)
        {
            WarehouseRoomEntity? warehouse = await repository.GetByIdAsync(query.Id)
                ?? throw new KeyNotFoundException($"Склад с ID {query.Id} не найден");

            WarehouseRoomDto result = warehouse.Adapt<WarehouseRoomDto>();
            return result;
        }
    }
}