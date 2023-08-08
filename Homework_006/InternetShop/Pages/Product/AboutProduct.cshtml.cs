using InternetShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InternetShop.Pages.Product
{
    public class AboutProductModel : PageModel
    {
        private readonly DataHandler _dataHandler;
        public required Models.Product Product { get; set; }

        public AboutProductModel(ApplicationContext db)
        {
            _dataHandler = new DataHandler(db);
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Models.Product? product = await _dataHandler.GetProductAsync(id);

            if (product == null)
                RedirectToPage("/Error");
            else
                Product = product;

            return Page();
        }
    }
}
