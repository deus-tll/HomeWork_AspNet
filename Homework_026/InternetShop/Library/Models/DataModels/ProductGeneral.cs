namespace Library.Models.DataModels
{
    public class ProductGeneral
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public double DiscountPercentage { get; set; }
        public double Rating { get; set; }
        public int Stock { get; set; }
        public string? Thumbnail { get; set; }
    }
}
