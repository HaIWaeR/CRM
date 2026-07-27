using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Behavior.Product
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Category { get; set; }
        public string? Article { get; set; }
        public string? Description { get; set; }
        public Dictionary<string, string>? Attributes { get; set; }
        public bool IsService { get; set; }
    }

    public class CreateProductCommandHandler(IProductRepository repository) : IRequestHandler<CreateProductCommand, Guid>
    {
        public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(command.Article))
            {
                ProductEntity? existing = await repository.GetByArticleAsync(command.Article);
                if (existing != null)
                    throw new InvalidOperationException($"Артикул '{command.Article}' уже существует");
            }

            ProductEntity product = command.Adapt<ProductEntity>();
            product.Id = Guid.NewGuid();
            product.IsActive = true;
            product.CreatedAt = DateTime.UtcNow;

            await repository.AddAsync(product);
            return product.Id;
        }
    }


}