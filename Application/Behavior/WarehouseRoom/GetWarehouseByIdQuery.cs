using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Behavior.WarehouseRoom
{
    public class GetWarehouseRoomByIdQuery : IRequest<WarehouseRoomDto?>
    {
        public Guid Id { get; set; }
    }

    public class GetWarehouseRoomByIdQueryHandler(IWarehouseRoomRepository repository) : IRequestHandler<GetWarehouseRoomByIdQuery, WarehouseRoomDto?>
    {
        public async Task<WarehouseRoomDto?> Handle(GetWarehouseRoomByIdQuery query, CancellationToken cancellationToken)
        {
            WarehouseRoomEntity? warehouse = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Склад с ID {query.Id} не найден");
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