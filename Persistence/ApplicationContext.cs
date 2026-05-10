using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
        }
        public DbSet<BranchEntity> Branchs { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<MaterialEntity> Materials { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<StockItemEntity> StockItems { get; set; }
        public DbSet<StorageZone> Storages { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<WarehouseEntity> Warehouses { get; set; }
    }
}
