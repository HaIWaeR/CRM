using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Product
{
    public class UpdateProductCommand : IRequest<ProductEntity?>
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

    public class UpdateProductCommandHandler(IProductRepository repository) : IRequestHandler<UpdateProductCommand, ProductEntity?>
    {
        public async Task<ProductEntity?> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
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

            return product;
        }
    }
}