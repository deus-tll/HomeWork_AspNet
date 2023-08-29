using InternetShop.Models.DataModels;

namespace InternetShop.Models.ViewModels
{
    public class ViewMessagesViewModel
    {
        public required List<Message> Messages { get; set; }
        public required Func<string, Task<string?>> UserEmailGetter { get; set; }
    }
}
