using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using WebApi.DTO.Product;

namespace Application.Behavior.Product
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public Guid Id { get; set; }
    }

    public class GetProductByIdQueryHandler(IProductRepository repository) : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        public async Task<ProductDto> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            ProductEntity? product = await repository.GetByIdAsync(query.Id)
                ?? throw new KeyNotFoundException($"Товар с ID {query.Id} не найден");

            ProductDto result = product.Adapt<ProductDto>();
            return result;
        }
    }
}