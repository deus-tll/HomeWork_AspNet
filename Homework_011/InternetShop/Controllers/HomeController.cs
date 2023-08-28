using InternetShop.Models;
using InternetShop.Models.DataModels;
using InternetShop.Models.HandlerModels;
using InternetShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserHandler _userHandler;
        private readonly ProductHandler _productHandler;


        public HomeController(ApplicationContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userHandler = new(userManager, signInManager, context);
            _productHandler = new(context);
        }


        public async Task<IActionResult> Index()
        {
            var products = await _productHandler.GetProductsAsync();
            var filter = new ProductFilter();
            var viewModel = new IndexViewModel { Products = products, Filter = filter };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> FilterProducts(ProductFilter filter)
        {
            var products = await _productHandler.GetFilteredProductsAsync(filter);
            var viewModel = new IndexViewModel { Products = products, Filter = filter };

            return View("Index", viewModel);
        }
    }
}