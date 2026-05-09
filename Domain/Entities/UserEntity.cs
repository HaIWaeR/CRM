using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public UserRole Role { get; private set; }
        public string? Email { get; private set; }
        public bool IsActive { get; private set; }
        public string PasswordHash { get; private set; }
        public string? Branch { get; private set; }
        public DateTime CreateAt { get; }
        public DateTime? UpdateAt { get; private set; }
    }
}
