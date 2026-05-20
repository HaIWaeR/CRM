using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Behavior.Product
{
    public class GetProductByIdQuery : IRequest<ProductEntity?>
    {
        public Guid Id { get; set; }
    }

    public class GetProductByIdQueryHandler(IProductRepository repository) : IRequestHandler<GetProductByIdQuery, ProductEntity?>
    {
        public async Task<ProductEntity?> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            ProductEntity? product = await repository.GetByIdAsync(query.Id) ?? throw new Exception($"Товар с ID {query.Id} не найден");

            return product;
        }
    }
}