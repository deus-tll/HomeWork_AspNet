using Microsoft.AspNetCore.Mvc;
using Task_1.Models;

namespace Task_1.Controllers
{
    public class HomeController : Controller
    {
        public async Task Index()
        {
            Response.ContentType = "text/html;charset=utf-8";

            CurrentMachineOS currentMachineOS = new();

            await Response.WriteAsync($"<h2>NAME OF CURRENT SERVER'S OS: {currentMachineOS.OS}</h2>" +
                $"<h2><strong>VERSION OF CURRENT SERVER'S OS: </strong>{currentMachineOS.OS_Version}</h2>");
        }
    }
}
