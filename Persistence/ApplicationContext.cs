using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ApplicationContext : DbContext
    {
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
    }
}
