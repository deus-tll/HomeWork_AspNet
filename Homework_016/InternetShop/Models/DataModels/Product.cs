using System.ComponentModel.DataAnnotations;

namespace InternetShop.Models.DataModels
{
    public class Product
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;


        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = string.Empty;


        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; } = string.Empty;


        [Required(ErrorMessage = "Brand is required.")]
        public string Brand { get; set; } = string.Empty;


        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be a non-negative integer.")]
        public int StockQuantity { get; set; } 


        [Range(1900, 9999, ErrorMessage = "Invalid year of manufacture.")]
        public int YearOfManufacture { get; set; }
    }
}
