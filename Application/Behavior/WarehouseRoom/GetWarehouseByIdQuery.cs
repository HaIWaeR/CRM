using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.WarehouseRoom
{
    public class GetWarehouseRoomByIdQuery : IRequest<WarehouseRoomEntity?>
    {
        public Guid Id { get; set; }
    }

    public class GetWarehouseRoomByIdQueryHandler(IWarehouseRoomRepository repository) : IRequestHandler<GetWarehouseRoomByIdQuery, WarehouseRoomEntity?>
    {
        public async Task<WarehouseRoomEntity?> Handle(GetWarehouseRoomByIdQuery query, CancellationToken cancellationToken)
        {
            WarehouseRoomEntity? warehouse = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Склад с ID {query.Id} не найден");
            
            return warehouse;
        }
    }
}