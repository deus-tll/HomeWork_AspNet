using InternetShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Components
{
    public class ConfirmActionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string header, string controllerName, string actionName, List<MyButton> buttons)
        {
            var model = new ConfirmActionViewModel
            {
                Header = header,
                ControllerName = controllerName,
                ActionName = actionName,
                Buttons = buttons
            };

            return View(model);
        }
    }
}
