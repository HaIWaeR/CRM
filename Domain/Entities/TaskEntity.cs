using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }

        public Guid? ClientId { get; set; }
        public ClientEntity? Client { get; set; }

        public Guid? UserId { get; set; }
        public UserEntity? User { get; set; }
    }
}
