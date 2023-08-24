using System.ComponentModel.DataAnnotations;

namespace Practice.Models.ViewModels
{
    public class MakeReviewViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field is required")]
        public string Text { get; set; } = string.Empty;
    }
}
