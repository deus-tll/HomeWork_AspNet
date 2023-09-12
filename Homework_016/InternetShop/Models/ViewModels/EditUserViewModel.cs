using System.ComponentModel.DataAnnotations;

namespace InternetShop.Models.ViewModels
{
    public class EditUserViewModel
    {
        public required string Id { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [Display(Name = "Email")]
        public required string Email { get; set; }
        
        public string UserName { get; set; }

        [Required(ErrorMessage = "The Year of Birth field is required.")]
        [CurrentYearMaxValue(1900, ErrorMessage = "Please enter a valid year of birth.")]
        [Display(Name = "Year of Birth")]
        public required int YearOfBirth { get; set; }
    }
}
