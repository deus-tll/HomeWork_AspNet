using Library.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages
{
    public class AboutModel : PageModel
    {
        private readonly ApiRequestHelper _apiRequestHelper;

        public required Comic Comic { get; set; }

        public AboutModel(IConfiguration configuration) => _apiRequestHelper = new ApiRequestHelper(configuration);

        public async Task OnGet(int id)
        {
            var result = await _apiRequestHelper.GetComic(id);

            if (result != null)
                Comic = result;
            else
                RedirectToPage("/Error");
        }
    }
}