using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InternetShop.Pages.Account
{
    public class DashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
