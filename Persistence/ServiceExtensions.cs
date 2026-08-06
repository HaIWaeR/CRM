using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;
using Persistence.Services;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStockItemRepository, StockItemRepository>();
            services.AddScoped<IStorageZoneRepository, StorageZoneRepository>();
            services.AddScoped<ISupplierMaterialRepository, SupplierMaterialRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWarehouseRoomRepository, WarehouseRoomRepository>();

            services.Configure<JwtSettings>(options =>
            {
                options.Secret = configuration.GetSection("JwtSettings")["Secret"] ?? string.Empty;
                options.Issuer = configuration.GetSection("JwtSettings")["Issuer"] ?? string.Empty;
                options.Audience = configuration.GetSection("JwtSettings")["Audience"] ?? string.Empty;
                options.ExpiryMinutes = int.Parse(configuration.GetSection("JwtSettings")["ExpiryMinutes"] ?? "60");
            });
            services.AddScoped<IJwtService, JwtService>();

            services.Configure<PaginationSettings>(configuration.GetSection("Pagination"));

            return services;
        }
    }
}