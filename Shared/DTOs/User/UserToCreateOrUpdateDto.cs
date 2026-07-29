using Domain.Enums;

namespace Shared.DTOs.User
{
    public class UserToCreateOrUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public Guid? BranchId { get; set; }
    }
}