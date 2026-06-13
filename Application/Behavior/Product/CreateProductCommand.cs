using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Product
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public string? Article { get; set; }
        public string? Description { get; set; }
        public Dictionary<string, string>? Attributes { get; set; }
        public bool IsActive { get; set; }
        public bool IsService { get; set; }
    }

    public class CreateProductCommandHandler(IProductRepository repository) : IRequestHandler<CreateProductCommand, Guid>
    {
        public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            ProductEntity product = new ProductEntity
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Price = command.Price,
                Category = command.Category,
                Article = command.Article,
                Description = command.Description,
                Attributes = command.Attributes,
                IsActive = command.IsActive,
                IsService = command.IsService,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(product);
            return product.Id;
        }
    }


}