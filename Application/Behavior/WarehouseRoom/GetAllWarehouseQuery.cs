using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.WarehouseRoom;

namespace Application.Behavior.WarehouseRoom
{
    public class GetAllWarehouseRoomsQuery : IRequest<List<WarehouseRoomDto>>
    {
        public string? SearchTerm { get; set; }
        public Guid? BranchId { get; set; }
        public WarehouseStatus? Status { get; set; }
    }

    public class GetAllWarehouseRoomsQueryHandler(IWarehouseRoomRepository repository) : IRequestHandler<GetAllWarehouseRoomsQuery, List<WarehouseRoomDto>>
    {
        public async Task<List<WarehouseRoomDto>> Handle(GetAllWarehouseRoomsQuery query, CancellationToken cancellationToken)
        {
            List<WarehouseRoomEntity> warehouses = await repository.GetFilteredAsync(
                query.SearchTerm,
                query.BranchId,
                query.Status);

            List<WarehouseRoomDto> result = warehouses.Adapt<List<WarehouseRoomDto>>();
            return result;
        }
    }
}