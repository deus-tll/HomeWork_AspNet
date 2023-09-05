using System.ComponentModel.DataAnnotations;

namespace InternetShop.Models.DataModels
{
    public class Message
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Message field is required.")]
        public required string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public required string UserId { get; set; }
    }
}
