namespace WebApi.DTO.StockItem
{
    public class StockItemToCreateOrUpdateDto
    {
        public int Quantity { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid? StorageZoneId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? MaterialId { get; set; }
    }
}