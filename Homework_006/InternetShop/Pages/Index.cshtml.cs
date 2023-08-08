using InternetShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Reflection.Metadata.BlobBuilder;

namespace InternetShop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DataHandler _handler;

        public IndexModel(ApplicationContext context)
        {
            _handler = new DataHandler(context);
        }

        public required List<Models.Product> Products { get; set; }

        [BindProperty]
        public required ProductFilter Filter { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Products = await _handler.GetProductsAsync();

            return Page();
        }

        public async Task OnPostAsync()
        {
            Products = await _handler.GetFilteredProductsAsync(Filter);
        }
    }
}