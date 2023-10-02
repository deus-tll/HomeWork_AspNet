using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Поле 'Ім'я' є обов'язковим.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле 'Електронна пошта' є обов'язковим.")]
        [EmailAddress(ErrorMessage = "Невірний формат електронної пошти.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле 'Вид процедури' є обов'язковим.")]
        public string Procedure { get; set; }

        [Required(ErrorMessage = "Поле 'Повідомлення' є обов'язковим.")]
        public string Message { get; set; }

        public bool IsSent { get; set; } = false;
    }
}
