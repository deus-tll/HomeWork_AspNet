namespace InternetShop.Models.ViewModels
{
    public class ConfirmActionViewModel
    {
        public string Header { get; set; } = string.Empty;
        public string ControllerName { get; set; } = string.Empty;
        public string ActionName { get; set; } = string.Empty;
        public required List<MyButton> Buttons { get; set; }
    }

    public class MyButton
    {
        public string Text { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
