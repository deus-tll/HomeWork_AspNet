using InternetShop.Models;
using InternetShop.Models.DataModels;
using InternetShop.Models.HandlerModels;
using InternetShop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

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


        [HttpGet]
        [Route("{controller=Home}/{action=Index}")]
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


        [HttpGet]
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


        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Home/Account/Login.cshtml");
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


        [HttpGet]
        public IActionResult Logout()
        {
            return View("~/Views/Home/Account/Logout.cshtml");
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmLogout(string action)
        {
            if (action == "logout")
                await _userHandler.LogoutAsync();

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult DeleteUser()
        {
            return View("~/Views/Home/Account/DeleteUser.cshtml");
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmDeleteUser(string action)
        {
            if (action == "delete")
                await _userHandler.DeleteUserAsync(User.Identity?.Name);
            else if (action == "cancel")
                return RedirectToAction("Dashboard", "Home");

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Dashboard()
        {
            return View("~/Views/Home/Account/Dashboard.cshtml");
        }


        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult AddProduct()
        {
            var model = new ProductViewModel() { Product = new() };
            return View("~/Views/Home/Product/AddProduct.cshtml", model);
        }


        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/Product/AddProduct.cshtml", model);
            }

            await _productHandler.AddProductAsync(model.Product);

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productHandler.GetProductAsync(id);

            if (product == null) return NotFound();

            var viewModel = new ProductViewModel { Product = product };
            return View("~/Views/Home/Product/EditProduct.cshtml", viewModel);
        }


        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> EditProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _productHandler.UpdateProductAsync(model.Product);
                return RedirectToAction("AboutProduct", new { id = model.Product.Id });
            }

            return View("~/Views/Home/Product/EditProduct.cshtml", model);
        }


        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productHandler.GetProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel { Product = product };
            return View("~/Views/Home/Product/DeleteProduct.cshtml", viewModel);
        }


        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteProduct(ProductViewModel model)
        {
            await _productHandler.DeleteProductAsync(model.Product);

            return RedirectToAction("Index");
        }


        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> ViewMessages()
        {
            var messages = await _userHandler.GetMessages();

            var messagesViewModel = new ViewMessagesViewModel
            {
                Messages = messages,
                UserEmailGetter = async userId => await _userHandler.GetUserEmailByIdAsync(userId)
            };

            return View("~/Views/Home/Message/ViewMessages.cshtml", messagesViewModel);
        }


        [HttpGet]
        public IActionResult WriteMessage()
        {
            return View("~/Views/Home/Message/WriteMessage.cshtml", new WriteMessageViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> WriteMessage(WriteMessageViewModel model)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Home/Message/WriteMessage.cshtml", model);

            string result = await _userHandler.SendMessageToManager(User, model.MessageText);

            await Console.Out.WriteLineAsync("CJKJDBCKJDCBJKDKJDKJBCDK");
            return RedirectToAction(result, "Home", new { area = "" });
        }
    }
}