using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Set name for person")]
        public string Name { get; set; } = string.Empty;

        [Range(1, 100, ErrorMessage = "Age should be in range from 1 to 100")]
        [Required(ErrorMessage = "Set age for person")]
        public int Age { get; set; }
    }
}
