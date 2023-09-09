using InternetShop.Models.DataModels;
using InternetShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Components
{
    public class ProductListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<Product> products)
        {
            var model = new ProductListViewModel
            {
                Products = products
            };

            return View(model);
        }
    }
}
