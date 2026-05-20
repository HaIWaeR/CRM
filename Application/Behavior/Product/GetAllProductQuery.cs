using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.Product
{
    public class GetAllProductsQuery : IRequest<List<ProductEntity>>;

    public class GetAllProductsQueryHandler(IProductRepository repository) : IRequestHandler<GetAllProductsQuery, List<ProductEntity>>
    {
        public async Task<List<ProductEntity>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            List<ProductEntity> products = await repository.GetAllAsync();

            return products;
        }
    }
}