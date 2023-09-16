using System.ComponentModel.DataAnnotations;

namespace AboutCSharp.Models.DataModels
{
    public class Review
    {
        [Required(ErrorMessage = "The \"Email\" is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The \"Text\" is required")]
        public string Text { get; set; } = string.Empty;
    }
}
