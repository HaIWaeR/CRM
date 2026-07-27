using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Order
{
    public class OrderToCreateOrUpdateDto
    {
        public string ServiceName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Address { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? BranchId { get; set; }
        public List<OrderItemRequestDto> OrderItems { get; set; } = [];
    }
}
