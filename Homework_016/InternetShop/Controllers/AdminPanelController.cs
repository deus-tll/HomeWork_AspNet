using InternetShop.Models.HandlerModels;
using InternetShop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private readonly IUserHandler _userHandler;

        public AdminPanelController(IUserHandler userHandler)
        {
            _userHandler = userHandler;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userHandler.GetAllUsersInRoleAsync("User");
            return View(users);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHandler.SignUpUserAsync(model.Input.Email, model.Input.Password, model.Input.YearOfBirth, "User");

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "AdminPanel");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userHandler.GetUserByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var user = await _userHandler.GetUserByIdAsync(id);
            if (user != null)
            {
                await _userHandler.DeleteUserAsync(user.UserName, false);
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userHandler.GetUserByIdAsync(id);

            if (user != null)
            {
                var model = new EditUserViewModel { Id = user.Id, Email = user.Email, UserName = user.UserName, YearOfBirth = user.YearOfBirth };
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHandler.EditUserAsync(model.Id, model.Email, model.UserName, model.YearOfBirth);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
    }
}
