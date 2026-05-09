namespace Domain.Entities
{
    public class StockItemEntity
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid? StorageZoneId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? MaterialId { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
