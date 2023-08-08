using InternetShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InternetShop.Pages.Message
{
    [Authorize(Roles = "User")]
    public class WriteMessageModel : PageModel
    {
        private readonly DataHandler _handler;


        public WriteMessageModel (UserManager<IdentityUser> userManager, ApplicationContext context)
        {
            _handler = new(userManager, context);
        }


        [BindProperty]
        public required string MessageText { get; set; }


        public IActionResult OnGet()
        {
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            string result = await _handler.SendMessageToManager(User, MessageText);

            return RedirectToPage(result);
        }
    }
}