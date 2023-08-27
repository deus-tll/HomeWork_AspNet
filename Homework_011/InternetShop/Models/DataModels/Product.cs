using System.ComponentModel.DataAnnotations;

namespace InternetShop.Models.DataModels
{
    public class Product
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public required string Name { get; set; }


        [Required(ErrorMessage = "Description is required.")]
        public required string Description { get; set; }


        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Category is required.")]
        public required string Category { get; set; }


        [Required(ErrorMessage = "Brand is required.")]
        public required string Brand { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be a non-negative integer.")]
        public int StockQuantity { get; set; }


        [Range(1900, 9999, ErrorMessage = "Invalid year of manufacture.")]
        public int YearOfManufacture { get; set; }
    }
}
