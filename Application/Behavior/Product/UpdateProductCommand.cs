using Application.Interfaces.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using WebApi.DTO.Product;

namespace Application.Behavior.Product
{
    public class UpdateProductCommand : IRequest<ProductDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Category { get; set; }
        public string? Article { get; set; }
        public string? Description { get; set; }
        public Dictionary<string, string>? Attributes { get; set; }
        public bool IsService { get; set; }
    }

    public class UpdateProductCommandHandler(IProductRepository repository) : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        public async Task<ProductDto> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            ProductEntity? existing = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Товар с ID {command.Id} не найден");

            if (!string.IsNullOrWhiteSpace(command.Article))
            {
                ProductEntity? existingArticle = await repository.GetByArticleAsync(command.Article);
                if (existingArticle != null && existingArticle.Id != command.Id)
                    throw new InvalidOperationException($"Артикул '{command.Article}' уже существует");
            }

            command.Adapt(existing);
            existing.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(existing);

            ProductDto result = existing.Adapt<ProductDto>();
            return result;
        }
    }
}