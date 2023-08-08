using InternetShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace InternetShop.Pages.Product
{
    [Authorize(Roles = "Manager")]
    public class DeleteProductModel : PageModel
    {
        private readonly DataHandler _handler;

        public DeleteProductModel(ApplicationContext context)
        {
            _handler = new DataHandler(context);
        }

        [BindProperty]
        public required Models.Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Models.Product? product = await _handler.GetProductAsync(id);

            if (product == null)
                return NotFound();
            else
                Product = product;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _handler.DeleteProductAsync(Product);

            return RedirectToPage("/Index");
        }
    }
}
