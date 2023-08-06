using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.ControllerHandlers;
using Portfolio.Models.ViewModels;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeControllerHandler _handler;


        private readonly CoursesViewModel _coursesViewModel;
        private readonly PortfolioViewModel _portfolioViewModel;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _handler = new HomeControllerHandler(logger);
            _coursesViewModel = new();
            _portfolioViewModel = new();
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult About()
        {
            return View();
        }


        public IActionResult AdditionalInfo()
        {
            return View();
        }


        public async Task<IActionResult> Courses()
        {
            _coursesViewModel.Files = await _handler.GetFilePathsAsync();

            return View(_coursesViewModel);
        }


        public async Task<IActionResult> Portfolio()
        {
            _portfolioViewModel.Files = await _handler.GetFilesAsync();

            return View(_portfolioViewModel);
        }


         
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }


        //Реалізувати механізм отримання POST запиту з даними для формування email листа від відвідувача
        //і відправки його до певної email адреси власника сайту(яку можна зберігати в appsettings.json).
        //Також можна в подальшому додати невеличку базу даних, яка буде зберігати email адреси всіх хто її вводив,
        //та потім робити розсилку-сповіщення коли відбулись якісь зміни на сайті чи в цілому у власника,
        //наприклад: нові курси, зміни цін, переїзд офісу/кабінету, поповнення роботами портфоліо і т.д.

        //[HttpPost]
        //public IActionResult Contact(...)
        //{
        //    return View(...);
        //}
    }
}
