﻿using System.ComponentModel.DataAnnotations;

namespace InternetShop.Models.ViewModels
{
    public class CurrentYearMaxValueAttribute : RangeAttribute
    {
        public CurrentYearMaxValueAttribute(int minYear) : base(minYear, DateTime.Now.Year)
        {
            ErrorMessage = $"Please enter a year between {minYear} and {DateTime.Now.Year}.";
        }
    }

    public class SignUpViewModel
    {
        public required InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "The Email field is required.")]
            [EmailAddress(ErrorMessage = "Invalid Email Address.")]
            [Display(Name = "Email")]
            public required string Email { get; set; }

            [Required(ErrorMessage = "The Password field is required.")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public required string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public required string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "The Year of Birth field is required.")]
            [CurrentYearMaxValue(1900, ErrorMessage = "Please enter a valid year of birth.")]
            [Display(Name = "Year of Birth")]
            public required int YearOfBirth { get; set; }
        }
    }
}
