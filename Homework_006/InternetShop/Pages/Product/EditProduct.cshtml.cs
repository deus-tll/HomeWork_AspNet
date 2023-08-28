using InternetShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace InternetShop.Pages.Product
{
    [Authorize(Roles = "Manager")]
    public class EditProductModel : PageModel
    {
        private readonly DataHandler _handler;
        [BindProperty]
        public required Models.Product Product { get; set; }

        public EditProductModel(ApplicationContext context)
        {
            _handler = new DataHandler(context);
        }

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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _handler.UpdateProductAsync(Product);

            return RedirectToPage($"/Product/AboutProduct", new { id = Product.Id });
        }
    }
}