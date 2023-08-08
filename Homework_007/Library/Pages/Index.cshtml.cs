using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DataHandler _dataHandler;
        public required List<Book> Books { get; set; }

        [Microsoft.AspNetCore.Mvc.BindProperty]
        public required BookFilter Filter { get; set; }

        public IndexModel(ApplicationContext db)
        {
            _dataHandler = new DataHandler(db);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Books = await _dataHandler.GetBooksAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Books = await _dataHandler.GetFilteredBooksAsync(Filter);

            return Page();
        }
    }
}