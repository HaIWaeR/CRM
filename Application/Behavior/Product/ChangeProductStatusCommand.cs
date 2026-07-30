using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behavior.Product
{
    public class ChangeProductStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public ProductStatus Status { get; set; }
    }
    public class ChangeProductStatusCommandHandler(IProductRepository repository) : IRequestHandler<ChangeProductStatusCommand, bool>
    {
        public async Task<bool> Handle(ChangeProductStatusCommand command, CancellationToken cancellationToken)
        {
            ProductEntity? product = await repository.GetByIdAsync(command.Id)
                ?? throw new KeyNotFoundException($"Товар с ID {command.Id} не найден");

            product.Status = command.Status;
            product.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(product);
            return true;
        }
    }
}
