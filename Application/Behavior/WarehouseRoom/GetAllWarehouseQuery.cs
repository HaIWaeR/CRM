using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.WarehouseRoom
{
    public class GetAllWarehouseRoomsQuery : IRequest<List<WarehouseRoomDto>>;

    public class GetAllWarehouseRoomsQueryHandler(IWarehouseRoomRepository repository) : IRequestHandler<GetAllWarehouseRoomsQuery, List<WarehouseRoomDto>>
    {
        public async Task<List<WarehouseRoomDto>> Handle(GetAllWarehouseRoomsQuery query, CancellationToken cancellationToken)
        {
            List<WarehouseRoomEntity> warehouses = await repository.GetAllAsync();
            return warehouses.Select(warehouse => new WarehouseRoomDto
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
            }).ToList();
        }
    }
}