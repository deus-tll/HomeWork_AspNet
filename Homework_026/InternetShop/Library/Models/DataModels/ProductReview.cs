using Library.Models.ContextModels;

namespace Library.Models.DataModels
{
    public class ProductReview
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public int ProductId { get; set; }
        public ProductDb? Product { get; set; }
        public required string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
