using System.ComponentModel.DataAnnotations;

namespace InternetShop.Models.ViewModels
{
    public class WriteMessageViewModel
    {
        [Required(ErrorMessage = "Message text is required.")]
        public string MessageText { get; set; } = string.Empty;
    }
}
