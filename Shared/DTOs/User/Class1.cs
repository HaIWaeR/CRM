using Domain.Enums;

namespace Shared.DTOs.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? BranchId { get; set; }
        public string? BranchName { get; set; }
    }
}