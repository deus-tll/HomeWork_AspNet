using InternetShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Components
{
    public class DashboardListItemViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string paragraphContent, string controllerName, string actionName, string linkText, string linkClass)
        {
            var model = new DashboardListItemViewModel
            {
                ParagraphContent = paragraphContent,
                ControllerName = controllerName,
                ActionName = actionName,
                LinkText = linkText,
                LinkClass = linkClass
            };

            return View(model);
        }
    }
}
