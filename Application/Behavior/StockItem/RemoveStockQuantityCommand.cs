using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behavior.StockItem
{
    public class RemoveStockQuantityCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
    public class RemoveStockQuantityCommandHandler(IStockItemRepository repository) : IRequestHandler<RemoveStockQuantityCommand, bool>
    {
        public async Task<bool> Handle(RemoveStockQuantityCommand command, CancellationToken cancellationToken)
        {
            if (command.Quantity <= 0)
                throw new InvalidOperationException("Количество должно быть больше 0");

            await repository.RemoveQuantityAsync(command.Id, command.Quantity);
            return true;
        }
    }
}
