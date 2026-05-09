namespace Domain.Entities
{
    public class StockItemEntity
    {
        public Guid Id { get; }
        public int Quantity { get; private set; }
        public Guid WarehouseId { get; private set; }
        public Guid? StorageZoneId { get; private set; }
        public Guid? ProductId { get; private set; }
        public Guid? MaterialId { get; private set; }
        public DateTime LastUpdate { get; private set; }
    }
}
