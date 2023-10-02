namespace Library.Models.DataModels
{
    public class ProductDb : ProductGeneral
    {
        public int Orders { get; set; }
        public int Views { get; set; }
        public DateTime DateAdded { get; set; }
        public int BrandId { get; set; }
        public required Brand Brand { get; set; }
        public int CategoryId { get; set; }
        public required Category Category { get; set; }
        public List<ProductImage>? Images { get; set; }

        public decimal CalculateDiscountedPrice()
        {
            decimal discountAmount = Price * (decimal)(DiscountPercentage / 100);

            return Price - discountAmount;
        }
    }
}
