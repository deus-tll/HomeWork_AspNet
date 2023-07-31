using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly DataHandler _dataHandler;
        [BindProperty]
        public required User MyUser { get; set; }

        public RegisterModel(ApplicationContext db)
        {
            _dataHandler = new DataHandler(db);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingUser = await _dataHandler.GetUserByEmailAsync(MyUser.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("MyUser.Email", "Email address already exists.");
                return Page();
            }

            await _dataHandler.AddUserAsync(MyUser);

            return RedirectToPage("/Users");
        }
    }
}
