using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WarehouseEntity
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public bool isActive { get; private set; }
        public string? Description { get; private set; }
        public string? Address { get; private set; }
        public string? ContactPerson { get; private set; }
        public string? ContactPhone { get; private set; }
        public DateTime CreateAt { get; }
        public DateTime UpdateAt { get; private set; }
    }
}
