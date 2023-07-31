namespace Library.Models
{
    public class BookFilter
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public string? Genre { get; set; }
        public int? MinPageCount { get; set; }
        public int? MaxPageCount { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public string? SortBy { get; set; }
        public bool IsSortAscending { get; set; }
    }
}
