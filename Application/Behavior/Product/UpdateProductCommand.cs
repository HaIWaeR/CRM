using Application.DTO;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Product
{
    public class UpdateProductCommand : IRequest<ProductDto?>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public string? Article { get; set; }
        public string? Description { get; set; }
        public Dictionary<string, string>? Attributes { get; set; }
        public bool IsActive { get; set; }
        public bool IsService { get; set; }
    }

    public class UpdateProductCommandHandler(IProductRepository repository) : IRequestHandler<UpdateProductCommand, ProductDto?>
    {
        public async Task<ProductDto?> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            ProductEntity? product = await repository.GetByIdAsync(command.Id) ?? throw new Exception($"Товар с ID {command.Id} не найден");
            product.Name = command.Name;
            product.Price = command.Price;
            product.Category = command.Category;
            product.Article = command.Article;
            product.Description = command.Description;
            product.Attributes = command.Attributes;
            product.IsActive = command.IsActive;
            product.IsService = command.IsService;
            product.UpdatedAt = DateTime.UtcNow;
            await repository.UpdateAsync(product);
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                Article = product.Article,
                Description = product.Description,
                Attributes = product.Attributes,
                IsActive = product.IsActive,
                IsService = product.IsService
            };
        }
    }
}