namespace Domain.Settings
{
    public class PaginationSettings
    {
        public int DefaultSize { get; set; }
        public int MaxSize { get; set; }
        public EntitySizes EntitySizes { get; set; } = new();
    }

    public class EntitySizes
    {
        public int Clients { get; set; }
        public int Orders { get; set; }
        public int Materials { get; set; }
        public int Products { get; set; }
        public int StockItems { get; set; }
        public int StorageZones { get; set; }
        public int Suppliers { get; set; }
        public int SupplierMaterials { get; set; }
        public int Tasks { get; set; }
        public int Users { get; set; }
        public int WarehouseRooms { get; set; }
        public int Branches { get; set; }
    }
}