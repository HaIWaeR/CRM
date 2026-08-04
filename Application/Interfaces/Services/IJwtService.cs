using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(UserEntity user);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }
}