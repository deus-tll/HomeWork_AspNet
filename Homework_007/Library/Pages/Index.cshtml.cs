using Library.Models;
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

        public async Task OnGetAsync()
        {
            Books = await _dataHandler.GetBooksAsync();
        }

        public async Task OnPostAsync()
        {
            Books = await _dataHandler.GetFilteredBooksAsync(Filter);
        }
    }
}