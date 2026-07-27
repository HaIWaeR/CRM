namespace Domain.Enums
{
    public enum StorageZoneStatus
    {
        Empty = 1,              // Свободна
        PartiallyOccupied = 2,  // Что-то лежит, но место есть
        FullyOccupied = 3,      // Занята полностью
        Maintenance = 4         // Обслуживается
    }
}