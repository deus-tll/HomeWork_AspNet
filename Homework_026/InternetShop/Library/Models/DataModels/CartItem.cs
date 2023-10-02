using Library.Models.ContextModels;

namespace Library.Models.DataModels
{
    public class CartItem
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public ProductDb? Product { get; set; }
    }
}
