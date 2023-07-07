using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class Task2Model : PageModel
    {
        public string Greetings { get; set; } = string.Empty;
        public void OnGet()
        {
            Greetings = "Hello, World!";
		}
    }
}
