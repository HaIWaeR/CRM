using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Persistence.Seeders
{
    public static class TesterSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            SeedTesterSettings settings = services.GetRequiredService<IOptions<SeedTesterSettings>>().Value;
            ApplicationContext db = services.GetRequiredService<ApplicationContext>();
            IJwtService jwtService = services.GetRequiredService<IJwtService>();

            bool testerExists = await db.Users.AnyAsync(u => u.Email == settings.Email);
            if (testerExists)
                return;

            UserEntity owner = new UserEntity
            {
                Id = Guid.NewGuid(),
                Name = settings.Name,
                Email = settings.Email,
                PasswordHash = jwtService.HashPassword(settings.Password),
                Role = UserRole.Owner,
                Status = UserStatus.Active,
                CreatedAt = DateTime.UtcNow
            };

            db.Users.Add(owner);
            await db.SaveChangesAsync();
        }
    }
}