using Library.Interfaces;
using Library.Models.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IUsersHandler _userHandler;
        private readonly IProductsHandler _productHandler;


        public CartController(IUsersHandler userHandler, IProductsHandler productHandler)
        {
            _userHandler = userHandler;
            _productHandler = productHandler;
        }

        [HttpGet]
        public IActionResult ViewCart()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var user = await _userHandler.GetUserAsync(User);

            if (user is null)
                return Json(new { success = false, error = "User was not found." });

            var product = await _productHandler.GetProductById(productId);

            if (product is null)
                return Json(new { success = false, error = "Product was not found." });

            var cartItem = new CartItem
            {
                UserId = user.Id,
                ProductId = productId,
                Quantity = 1
            };

            await _productHandler.AddCartItem(cartItem);

            int cartCount = await _productHandler.GetCountProductsInCart(user.Id);

            return Json(new { success = true, message = "Product was added to cart.", cartCount });
        }


        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
