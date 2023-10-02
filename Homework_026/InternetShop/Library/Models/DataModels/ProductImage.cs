namespace Library.Models.DataModels
{
    public class ProductImage
    {
        public int Id { get; set; }
        public required string Url { get; set; }
        public int ProductId { get; set; }
        public ProductDb? Product { get; set; }
    }
}
