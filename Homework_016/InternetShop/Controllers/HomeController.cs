using InternetShop.Filters;
using InternetShop.Models;
using InternetShop.Models.DataModels;
using InternetShop.Models.HandlerModels;
using InternetShop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace InternetShop.Controllers
{
    [AuthorizeHomeController]
    [ServiceFilter(typeof(MyExceptionFilter))]
    public class HomeController : Controller
    {
        private readonly IUserHandler _userHandler;
        private readonly IProductHandler _productHandler;


        public HomeController(IUserHandler userHandler, IProductHandler productHandler)
        {
            _userHandler = userHandler;
            _productHandler = productHandler;
        }


        [HttpGet]
        [ServiceFilter(typeof(LogActionFilterAttribute))]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 7)
        {
            List<Product> products;
            ProductFilter? filter = null;

            if (TempData.ContainsKey("Filter"))
            {
                var filterJson = TempData["Filter"] as string;
                if (!string.IsNullOrEmpty(filterJson))
                {
                    filter = JsonConvert.DeserializeObject<ProductFilter>(filterJson);
                    TempData["Filter"] = JsonConvert.SerializeObject(filter);
                }
            }

            if (filter is not null)
                products = await _productHandler.GetFilteredProductsAsync(filter);
            else
                products = await _productHandler.GetProductsAsync();

            var viewModel = GetIndexViewModel(filter ?? new ProductFilter(), products, page, pageSize, "Index", false);

            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> ProductsFiltering(ProductFilter filter, int page = 1, int pageSize = 7)
        {
            TempData["Filter"] = JsonConvert.SerializeObject(filter);

            var products = await _productHandler.GetFilteredProductsAsync(filter);

            var viewModel = GetIndexViewModel(filter, products, page, pageSize, "Index", true);

            return View("Index", viewModel);
        }

        private static IndexViewModel GetIndexViewModel(ProductFilter filter, List<Product> products, int page, int pageSize, string action, bool isFiltered)
        {
            var paginatedProducts = PaginatedList<Product>.Create(products.AsQueryable(), page, pageSize, action);
            var sortByOptions = GetSortByOptions(filter.SortBy);

            var viewModel = new IndexViewModel
            {
                Filter = filter,
                SortByOptions = sortByOptions,
                IsFiltered = isFiltered,
                PaginatedList = paginatedProducts
            };

            return viewModel;
        }


        [HttpGet]
        public async Task<IActionResult> TableProducts(int page = 1, int pageSize = 7)
        {
            var products = await _productHandler.GetProductsAsync();

            var viewModel = GetIndexViewModel(new ProductFilter(), products, page, pageSize, "TableProducts", false);

            return View(viewModel);
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
        [Authorize(Roles = "Manager, Admin")]
        public IActionResult AddProduct()
        {
            var model = new ProductViewModel() { Product = new() };
            return View("~/Views/Home/Product/AddProduct.cshtml", model);
        }


        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
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
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productHandler.GetProductAsync(id);

            if (product == null) return NotFound();

            var viewModel = new ProductViewModel { Product = product };
            return View("~/Views/Home/Product/EditProduct.cshtml", viewModel);
        }


        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
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
        [Authorize(Roles = "Manager, Admin")]
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
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> DeleteProduct(ProductViewModel model)
        {
            await _productHandler.DeleteProductAsync(model.Product);

            return RedirectToAction("Index");
        }


        [HttpGet]
        [Authorize(Roles = "Manager, Admin")]
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

            return RedirectToAction(result, "Home", new { area = "" });
        }
    }
}