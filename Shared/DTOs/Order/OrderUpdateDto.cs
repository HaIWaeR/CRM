using System;
using System.Collections.Generic;
using System.Linq;
namespace Shared.DTOs.Order
{
    public class OrderUpdateDto
    {
        public string ServiceName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Address { get; set; }
    }
}
