using Library.Interfaces;
using Library.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsHandler _productsHandler;

        public HomeController(IProductsHandler productsHandler)
        {
            _productsHandler = productsHandler;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productsHandler.GetTrendyProducts();

            IndexViewModel model = new()
            {
                Products = products
            };

            return View(model);
        }


        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
    }
}
