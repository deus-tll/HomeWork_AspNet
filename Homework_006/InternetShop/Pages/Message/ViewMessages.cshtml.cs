using InternetShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Pages.Message
{
    [Authorize(Roles = "Manager")]
    public class ViewMessagesModel : PageModel
    {
        private readonly DataHandler _handler;


        public ViewMessagesModel(UserManager<IdentityUser> userManager, ApplicationContext context)
        {
            _handler = new(userManager, context);
        }


        public required List<Models.Message> Messages { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            Messages = await _handler.GetMessages();

            return Page();
        }


        public async Task<string?> GetUserEmail(string userId)
        {
            return await _handler.GetUserEmail(userId);
        }
    }
}