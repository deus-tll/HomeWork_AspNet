using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages
{
    public class AboutModel : PageModel
    {
        private readonly DataHandler _dataHandler;
        public required Book Book { get; set; }

        public AboutModel(ApplicationContext db)
        {
            _dataHandler = new DataHandler(db);
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Book? book = await _dataHandler.GetBookAsync(id);

            if (book == null)
                RedirectToPage("/Error");
            else
                Book = book;

            return Page();
        }
    }
}
