namespace WebApi.DTO.User
{
    public class UserToCreateOrUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public Domain.Enums.UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public Guid? BranchId { get; set; }
    }
}