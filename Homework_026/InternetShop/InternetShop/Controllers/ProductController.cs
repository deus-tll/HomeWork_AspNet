using Library.Interfaces;
using Library.Models.DataModels;
using Library.Models.HandlerModels;
using Library.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUsersHandler _usersHandler;
        private readonly IProductsHandler _productsHandler;


        public ProductController(IUsersHandler usersHandler, IProductsHandler productsHandler)
        {
            _usersHandler = usersHandler;
            _productsHandler = productsHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Shop()
        {
            var products = await _productsHandler.GetProducts();
            var brands = await _productsHandler.GetBrands();
            var categories = await _productsHandler.GetCategories();

            List<DisplayBrand> displayBrands = new();
            List<DisplayCategory> displayCategories = new();

            foreach (var brand in brands)
            {
                DisplayBrand displayBrand = new() 
                {
                    Brand = brand,
                    Count = await _productsHandler.GetCountProductsByBrand(brand.Id)
                };

                displayBrands.Add(displayBrand);
            }

            foreach (var category in categories)
            {
                DisplayCategory displayCategory = new()
                {
                    Category = category,
                    Count = await _productsHandler.GetCountProductsByCategory(category.Id)
                };

                displayCategories.Add(displayCategory);
            }

            ShopViewModel model = new()
            {
                Products = products,
                Brands = displayBrands,
                Categories = displayCategories,
                AllProductsInBrands = displayBrands.Sum(b => b.Count),
                AllProductsInCategories = displayCategories.Sum(b => b.Count),
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult ProductDetails()
        {
            return View();
        }

        public async Task<IActionResult> GetCategories()
        {
            var categories = await _productsHandler.GetCategories();
            return Json(categories);
        }
    }
}
