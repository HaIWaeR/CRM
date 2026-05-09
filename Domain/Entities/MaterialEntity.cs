namespace Domain.Entities
{
    public class MaterialEntity
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public string Article { get; private set; }
        public int Quantity { get; private set; }
        public string? Description { get; private set; }
        public string UnitMeasurement { get; private set; }
        public decimal PriceUnit { get; private set; }
        public string? CellZone { get; private set; }
        public string? Supplier { get; private set; }
        public string? ContactSupplier { get; private set; }
        public string? AdditionInforamtion { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime? UpdatedAt { get; private set; }
    }
}
