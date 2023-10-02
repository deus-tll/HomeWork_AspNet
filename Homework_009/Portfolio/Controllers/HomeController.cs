using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.ControllerHandlers;
using Portfolio.Models.ViewModels;
using System.Net.Mail;
using System.Net;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeControllerHandler _handler;


        private readonly CoursesViewModel _coursesViewModel;
        private readonly PortfolioViewModel _portfolioViewModel;

        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _handler = new HomeControllerHandler(logger);
            _coursesViewModel = new();
            _portfolioViewModel = new();
            _configuration = configuration;
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
            if (TempData.ContainsKey("IsSent") && (bool)TempData["IsSent"])
            {
                ViewData["IsSent"] = true;
                TempData.Remove("IsSent");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {

                model.IsSent = false;

                string name = model.Name;
                string email = model.Email;
                string procedure = model.Procedure;
                string message = model.Message;

                string? smtpServer = _configuration["SmtpSettings:SmtpServer"];
                int.TryParse(_configuration["SmtpSettings:Port"], out int smtpPort);
                string? smtpUsername = _configuration["SmtpSettings:Username"];
                string? smtpPassword = _configuration["SmtpSettings:Password"];
                string? recipient = _configuration["MyData:Email"];

                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(email),
                        Subject = $"Нове повідомлення від {name}",
                        Body = $"Ім'я: {name}\nЕлектронна пошта: {email}\nВид процедури: {procedure}\nПовідомлення: {message}"
                    };

                    if (recipient is not null)
                    {
                        mailMessage.To.Add(recipient);

                        await client.SendMailAsync(mailMessage);

                        TempData["IsSent"] = true;
                    }
                }

                return RedirectToAction("Contact");
            }

            return View(model);
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
