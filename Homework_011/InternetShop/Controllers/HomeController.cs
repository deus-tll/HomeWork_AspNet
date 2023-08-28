using InternetShop.Models;
using InternetShop.Models.DataModels;
using InternetShop.Models.HandlerModels;
using InternetShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InternetShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserHandler _userHandler;
        private readonly ProductHandler _productHandler;


        public HomeController(IUserHandler userHandler, ApplicationContext context)
        {
            _userHandler = userHandler;
            _productHandler = new(context);
        }


        public async Task<IActionResult> Index()
        {
            var products = await _productHandler.GetProductsAsync();
            var filter = new ProductFilter();
            var sortByOptions = GetSortByOptions(filter.SortBy);
            var viewModel = new IndexViewModel 
            { 
                Products = products, 
                Filter = filter, 
                SortByOptions = sortByOptions 
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> FilterProducts(ProductFilter filter)
        {
            var products = await _productHandler.GetFilteredProductsAsync(filter);
            var sortByOptions = GetSortByOptions(filter.SortBy);
            var viewModel = new IndexViewModel 
            { 
                Products = products, 
                Filter = filter, 
                SortByOptions = sortByOptions
            };

            return View("Index", viewModel);
        }


        private static List<SelectListItem> GetSortByOptions(string? selectedValue)
        {
            var sortByOptions = new List<SelectListItem>()
            {
                new SelectListItem { Value = "--None--", Text = "--None--" },
                new SelectListItem { Value = "Name", Text = "Name" },
                new SelectListItem { Value = "Category", Text = "Category" },
                new SelectListItem { Value = "Brand", Text = "Brand" }
            };

            if (selectedValue is null)
            {
                sortByOptions[0].Selected = true;
                return sortByOptions;
            }

            foreach (var option in sortByOptions)
            {
                if (option.Value == selectedValue)
                {
                    option.Selected = true;
                    break;
                }
            }

            return sortByOptions;
        }


        public async Task<IActionResult> AboutProduct(int id)
        {
            var product = await _productHandler.GetProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new AboutProductViewModel { Product = product };
            return View("~/Views/Home/Product/AboutProduct.cshtml", viewModel);
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View("~/Views/Home/Account/SignUp.cshtml");
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHandler.SignUpUserAsync(model.Input.Email, model.Input.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View("~/Views/Home/Account/SignUp.cshtml", model);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var loginResult = await _userHandler.LoginAsync(model.Input.Email, model.Input.Password, model.Input.RememberMe);

            if (loginResult.Succeeded) 
                return RedirectToAction("Index", "Home");
            else
                ModelState.AddModelError(string.Empty, "Invalid login attempt");


            return View("~/Views/Home/Account/Login.cshtml", model);
        }


        public async Task<IActionResult> Logout()
        {
            await _userHandler.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}