using Library.Models.ContextModels;

namespace Library.Models.DataModels
{
    public enum OrderStatus
    {
        Accepted,
        Delivered,
        Received
    }

    public class Order
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public DateTime OrderedAt { get; set; }
        public List<CartItem>? OrderItems { get; set; }
        public OrderStatus Status { get; set; }
    }
}
