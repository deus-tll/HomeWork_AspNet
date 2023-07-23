namespace Library.Models
{
    public class Comic
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Format { get; set; }
        public DateTime OnSaleDate { get; set; }
        public required int PageCount { get; set; }
        public Cover? ComicCover { get; set; }
        public List<Price>? Prices { get; set; }
        public List<Creator>? Creators { get; set; }
    }
}
