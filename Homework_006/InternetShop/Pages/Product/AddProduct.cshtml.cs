using InternetShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace InternetShop.Pages.Product
{
    [Authorize(Roles = "Manager")]
    public class AddProductModel : PageModel
    {
        private readonly DataHandler _handler;
        [BindProperty]
        public required Models.Product Product { get; set; }

        public AddProductModel(ApplicationContext context)
        {
            _handler = new DataHandler(context);
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _handler.AddProductAsync(Product);

            return RedirectToPage("/Index");
        }
    }
}
