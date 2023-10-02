namespace Library.Models.DataModels
{
    public class ProductJson : ProductGeneral
    {
        public required string Brand { get; set; }
        public required string Category { get; set; }
        public List<string>? Images { get; set; }
    }
}
