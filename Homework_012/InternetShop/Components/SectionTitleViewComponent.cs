using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Components
{
    public class SectionTitleViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (ViewData["Title"] is string title && !string.IsNullOrEmpty(title))
            {
                return View("Default", title);
            }

            return View("Default", "Заголовок відсутній");
        }
    }
}
