using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Shared.DTOs.Supplier;

namespace Application.Behavior.Supplier
{
    public class GetAllSuppliersQuery : IRequest<List<SupplierDto>>
    {
        public string? SearchTerm { get; set; }
        public SupplierType? SupplierType { get; set; }
        public SupplierStatus? SupplierStatus { get; set; }
        public int? MinRating { get; set; }
        public int? MaxRating { get; set; }
    }

    public class GetAllSuppliersQueryHandler(ISupplierRepository repository) : IRequestHandler<GetAllSuppliersQuery, List<SupplierDto>>
    {
        public async Task<List<SupplierDto>> Handle(GetAllSuppliersQuery query, CancellationToken cancellationToken)
        {
            List<SupplierEntity> suppliers = await repository.GetFilteredAsync(
                query.SearchTerm,
                query.SupplierType,
                query.SupplierStatus,
                query.MinRating,
                query.MaxRating);

            List<SupplierDto> result = suppliers.Adapt<List<SupplierDto>>();
            return result;
        }
    }
}