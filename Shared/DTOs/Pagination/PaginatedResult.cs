namespace Shared.DTOs.Pagination
{
    /// <summary>
    /// Одна страница результатов списочного запроса вместе с данными для навигации по страницам.
    /// </summary>
    /// <typeparam name="T">Тип элементов на странице, например <c>ClientDto</c>.</typeparam>
    public class PaginatedResult<T>
    {
        /// <summary>
        /// Элементы текущей страницы. Пустой список, если страница за пределами результатов.
        /// </summary>
        public List<T> Items { get; set; } = [];

        /// <summary>
        /// Общее количество элементов, подходящих под фильтры, по всем страницам.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Номер текущей страницы, начиная с 1.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Максимальное количество элементов на странице.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Общее количество страниц; 0, если элементов нет.
        /// </summary>
        public int TotalPages { get; set; }
    }
}