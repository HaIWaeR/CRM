using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.Branch;
using Shared.DTOs.Pagination;

namespace Application.Behavior.Branch
{
    public class GetAllBranchesQuery : IRequest<PaginatedResult<BranchDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public string? SearchTerm { get; set; }
        public BranchStatus? Status { get; set; }
    }

    public class GetAllBranchesQueryHandler(IBranchRepository repository) : IRequestHandler<GetAllBranchesQuery, PaginatedResult<BranchDto>>
    {
        public async Task<PaginatedResult<BranchDto>> Handle(GetAllBranchesQuery query, CancellationToken cancellationToken)
        {
            List<BranchEntity> branches = await repository.GetFilteredAsync(
                query.SearchTerm,
                query.Status,
                query.Page,
                query.Size);

            int totalCount = await repository.GetTotalCountAsync(
                query.SearchTerm,
                query.Status);

            int totalPages = (int)Math.Ceiling(totalCount / (double)query.Size);

            if (query.Page > totalPages && totalPages > 0)
            {
                return new PaginatedResult<BranchDto>
                {
                    Items = new List<BranchDto>(),
                    TotalCount = totalCount,
                    Page = query.Page,
                    Size = query.Size,
                    TotalPages = totalPages
                };
            }

            List<BranchDto> items = branches.Adapt<List<BranchDto>>();

            return new PaginatedResult<BranchDto>
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