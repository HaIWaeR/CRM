using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.WarehouseRoom;
using Shared.DTOs.Pagination;

namespace Application.Behavior.WarehouseRoom
{
    public class GetAllWarehouseRoomsQuery : IRequest<PaginatedResult<WarehouseRoomDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public string? SearchTerm { get; set; }
        public Guid? BranchId { get; set; }
        public WarehouseStatus? Status { get; set; }
    }

    public class GetAllWarehouseRoomsQueryHandler(IWarehouseRoomRepository repository) : IRequestHandler<GetAllWarehouseRoomsQuery, PaginatedResult<WarehouseRoomDto>>
    {
        public async Task<PaginatedResult<WarehouseRoomDto>> Handle(GetAllWarehouseRoomsQuery query, CancellationToken cancellationToken)
        {
            List<WarehouseRoomEntity> warehouses = await repository.GetFilteredAsync(
                query.SearchTerm,
                query.BranchId,
                query.Status,
                query.Page,
                query.Size);

            int totalCount = await repository.GetTotalCountAsync(
                query.SearchTerm,
                query.BranchId,
                query.Status);

            int totalPages = (int)Math.Ceiling(totalCount / (double)query.Size);

            if (query.Page > totalPages && totalPages > 0)
            {
                return new PaginatedResult<WarehouseRoomDto>
                {
                    Items = new List<WarehouseRoomDto>(),
                    TotalCount = totalCount,
                    Page = query.Page,
                    Size = query.Size,
                    TotalPages = totalPages
                };
            }

            List<WarehouseRoomDto> items = warehouses.Adapt<List<WarehouseRoomDto>>();

            return new PaginatedResult<WarehouseRoomDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = query.Page,
                Size = query.Size,
                TotalPages = totalPages
            };
        }
    }
}