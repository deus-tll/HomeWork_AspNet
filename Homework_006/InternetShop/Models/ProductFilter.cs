namespace InternetShop.Models
{
    public class ProductFilter
    {
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Brand { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinStockQuantity { get; set; }
        public int? MaxStockQuantity { get; set; }
        public int? MinYearOfManufacture { get; set; }
        public int? MaxYearOfManufacture { get; set; }
        public string? SortBy { get; set; }
        public bool IsSortAscending { get; set; }
    }
}
