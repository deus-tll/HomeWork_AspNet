using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class Task3Model : PageModel
    {
        public DateTime CurrentDateTime { get; set; }
        public void OnGet()
        {
            CurrentDateTime = DateTime.Now;
        }
    }
}
