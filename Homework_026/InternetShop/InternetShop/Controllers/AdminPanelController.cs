using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
