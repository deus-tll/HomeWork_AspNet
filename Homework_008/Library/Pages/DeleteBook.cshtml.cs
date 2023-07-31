using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages
{
    public class DeleteBookModel : PageModel
    {
        private readonly DataHandler _dataHandler;

        public DeleteBookModel(ApplicationContext db)
        {
            _dataHandler = new DataHandler(db);
        }

        [BindProperty]
        public required Book Book { get; set; }

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
            await _dataHandler.DeleteBookAsync(Book);

            return RedirectToPage("Index");
        }
    }
}
