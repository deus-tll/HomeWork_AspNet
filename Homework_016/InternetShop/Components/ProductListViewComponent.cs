using InternetShop.Models;
using InternetShop.Models.DataModels;
using InternetShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Components
{
    public class ProductListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(PaginatedList<Product> paginatedList)
        {
            var model = new ProductListViewModel
            {
                PaginatedList = paginatedList
            };

            return View(model);
        }
    }
}
