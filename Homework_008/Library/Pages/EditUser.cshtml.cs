using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages
{
    public class EditUserModel : PageModel
    {
        private readonly DataHandler _dataHandler;
        [BindProperty]
        public required User MyUser { get; set; }
        private string _originalPassword = string.Empty;

        public EditUserModel(ApplicationContext db)
        {
            _dataHandler = new DataHandler(db);
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            User? user = await _dataHandler.GetUserAsync(id);



            if (user == null)
                return NotFound();
            else
            {
                MyUser = user;
                _originalPassword = user.Password;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(MyUser.Password))
            {
                ModelState.Remove("MyUser.Password");
            }

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

            MyUser.Password = _originalPassword;

            await _dataHandler.UpdateUserAsync(MyUser);

            return RedirectToPage($"/Users");
        }
    }
}
