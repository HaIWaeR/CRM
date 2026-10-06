using Domain.Entities;
using Domain.Entities.Supporting;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;

namespace Persistence
{
    /// <summary>
    /// Контекст EF Core для работы с базой данных CRM (PostgreSQL).
    /// Содержит наборы всех сущностей и применяет их конфигурации.
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Создаёт контекст с параметрами подключения, заданными при регистрации в DI.
        /// </summary>
        /// <param name="options">Параметры контекста, включая строку подключения <c>DefaultConnection</c>.</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<BranchEntity> Branches { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<MaterialEntity> Materials { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<StockItemEntity> StockItems { get; set; }
        public DbSet<StorageZoneEntity> Storages { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<WarehouseRoomEntity> Warehouses { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<SupplierMaterialEntity> SupplierMaterials { get; set; }
        public DbSet<SupplierEntity> Suppliers { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BranchConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new StockItemConfiguration());
            modelBuilder.ApplyConfiguration(new StorageZoneConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseRoomConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
        }
    }
}
