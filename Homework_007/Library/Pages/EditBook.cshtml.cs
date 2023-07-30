using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages
{
    public class EditBookModel : PageModel
    {
        private readonly DataHandler _dataHandler;
        [BindProperty]
        public required Book Book { get; set; }

        public EditBookModel(ApplicationContext db)
        {
            _dataHandler = new DataHandler(db);
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Book? book = await _dataHandler.GetBookAsync(id);

            if (book == null)
                return NotFound();
            else
                Book = book;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _dataHandler.UpdateBookAsync(Book);

            return RedirectToPage($"/About", new { id = Book.Id });
        }
    }
}
