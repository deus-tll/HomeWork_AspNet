using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [Required]
        public required string  Nickname { get; set; }

        [StringLength(256, MinimumLength = 1)]
        [EmailAddress]
        [Required]
        public required string Email { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [Required]
        public required string Password { get; set; }
    }
}