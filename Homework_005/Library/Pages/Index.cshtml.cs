using Library.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages
{
    public class IndexModel : PageModel, IDisposable
    {
        private readonly ApiRequestHelper _apiRequestHelper;
        public required List<Comic> Comics { get; set; }
        public IndexModel(IConfiguration configuration) => _apiRequestHelper = new ApiRequestHelper(configuration);


        public async Task OnGet()
        {
            List<Comic>? comics = await _apiRequestHelper.DefineData();
            if (comics is null)
            {
                RedirectToPage("/Error");
                return;
            }

            Comics = comics;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _apiRequestHelper.Dispose();
            }
        }
    }
}