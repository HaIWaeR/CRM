namespace Domain.Entities
{
    public class MaterialEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Article { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public string UnitMeasurement { get; set; }
        public decimal PriceUnit { get; set; }
        public string? CellZone { get; set; }
        public string? Supplier { get; set; }
        public string? ContactSupplier { get; set; }
        public string? AdditionInforamtion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
