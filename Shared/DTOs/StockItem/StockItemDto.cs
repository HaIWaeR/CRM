namespace Shared.DTOs.StockItem
{
    public class StockItemDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdate { get; set; }
        public Guid WarehouseId { get; set; }
        public string WarehouseName { get; set; } = string.Empty;
        public Guid? StorageZoneId { get; set; }
        public string? StorageZoneName { get; set; }
        public Guid? ProductId { get; set; }
        public string? ProductName { get; set; }
        public Guid? MaterialId { get; set; }
        public string? MaterialName { get; set; }
    }
}