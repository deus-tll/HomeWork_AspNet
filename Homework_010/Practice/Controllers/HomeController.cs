using Microsoft.AspNetCore.Mvc;
using Practice.Models;
using Practice.Models.HandlerModels;
using Practice.Models.ViewModels;
using System.Diagnostics;

namespace Practice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContentHandler _contentHandler;

        public HomeController(IWebHostEnvironment hostingEnvironment)
        {
            _contentHandler = new(hostingEnvironment);
        }

        public IActionResult Index()
        {
            return View(CreateMainContentViewModel(ContentHandler.NecessaryFiles.INDEX_CONTENT_PATH));
        }

        public IActionResult About()
        {
            return View(CreateMainContentViewModel(ContentHandler.NecessaryFiles.ABOUT_CONTENT_PATH));
        }

        public IActionResult Advantages()
        {
            return View(CreateMainContentViewModel(ContentHandler.NecessaryFiles.ADVANTAGES_CONTENT_PATH));
        }

        public IActionResult Areas()
        {
            return View(CreateMainContentViewModel(ContentHandler.NecessaryFiles.AREAS_CONTENT_PATH));
        }

        public IActionResult Apps()
        {
            ListRefsViewModel viewModel = new()
            {
                RefBlocks = _contentHandler.GetRefBlocks(ContentHandler.NecessaryFiles.APPS_CONTENT_PATH)
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult MakeReview()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MakeReview(MakeReviewViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            _contentHandler.SaveMessageToFile(viewModel);

            return RedirectToAction("Index");
        }

        public IActionResult Reviews()
        {
            WatchReviewsViewModel viewModel = new()
            {
                Messages = _contentHandler.ReadMessagesFromFile()
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private MainContentViewModel CreateMainContentViewModel(string path)
        {
            MainContentViewModel viewModel = new()
            {
                ContentBlocks = _contentHandler.GetContentBlocks(path)
            };

            return viewModel;
        }
    }
}