using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [Required]
        public required string Title { get; set; }

        [StringLength(600, MinimumLength = 1)]
        [Required]
        public required string Description { get; set; }

        [StringLength(150, MinimumLength = 1)]
        [Required]
        public required string Author { get; set; }

        [StringLength(200, MinimumLength = 1)]
        [Required]
        public required string Publisher { get; set; }

        [Range(1, int.MaxValue)]
        [Required]
        public required Int16 PageCount { get; set; }

        [StringLength(200, MinimumLength = 1)]
        [Required]
        public required string Genre { get; set; }

        [Range(1, 10000)]
        [Required]
        public Int16 Year { get; set; }
    }
}
