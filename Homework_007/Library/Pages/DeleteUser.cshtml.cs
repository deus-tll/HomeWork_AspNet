using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages
{
    public class DeleteUserModel : PageModel
    {
        private readonly DataHandler _dataHandler;

        [BindProperty]
        public required User MyUser { get; set; }

        public DeleteUserModel(ApplicationContext db)
        {
            _dataHandler = new DataHandler(db);
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            User? user = await _dataHandler.GetUserAsync(id);

            if (user == null)
                return NotFound();
            else
                MyUser = user;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _dataHandler.DeleteUserAsync(MyUser);

            return RedirectToPage("Users");
        }
    }
}
