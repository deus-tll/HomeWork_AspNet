using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages
{
    public class AddBookModel : PageModel
    {
        private readonly DataHandler _dataHandler;
        [BindProperty]
        public required Book Book { get; set; }

        public AddBookModel(ApplicationContext db)
        {
            _dataHandler = new DataHandler(db);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _dataHandler.AddBookAsync(Book);

            return RedirectToPage("Index");
        }
    }
}