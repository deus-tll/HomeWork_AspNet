using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages
{
    public class UsersModel : PageModel
    {
        private readonly DataHandler _dataHandler;

        public UsersModel(ApplicationContext db)
        {
            _dataHandler = new DataHandler(db);
        }

        public required List<User> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Users = await _dataHandler.GetUsersAsync();

            return Page();
        }
    }
}
