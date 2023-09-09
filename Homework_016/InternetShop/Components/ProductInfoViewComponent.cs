using InternetShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Components
{
    public class ProductInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(AboutProductViewModel model)
        {
            return View(model);
        }
    }
}
