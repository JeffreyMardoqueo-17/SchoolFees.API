namespace SchoolFees.API.Helpers
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        //aqui implementare para ordenar por asencente o descendente
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
         // 🧭 Campo y dirección del orden (solo informativo)
        public string? OrderBy { get; set; }
        public string? OrderDirection { get; set; } // "asc" o "desc"
    }
}
