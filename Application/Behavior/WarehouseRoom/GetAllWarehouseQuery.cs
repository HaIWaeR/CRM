using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.WarehouseRoom
{
    public class GetAllWarehouseRoomsQuery : IRequest<List<WarehouseRoomEntity>>;

    public class GetAllWarehouseRoomsQueryHandler(IWarehouseRoomRepository repository) : IRequestHandler<GetAllWarehouseRoomsQuery, List<WarehouseRoomEntity>>
    {
        public async Task<List<WarehouseRoomEntity>> Handle(GetAllWarehouseRoomsQuery query, CancellationToken cancellationToken)
        {
            List<WarehouseRoomEntity> warehouses = await repository.GetAllAsync();

            return warehouses;
        }
    }
}