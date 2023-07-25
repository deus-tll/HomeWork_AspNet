using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

//я знаю, що я тут один момент упустив, я дороблю
namespace Library.Pages
{
    public class AboutModel : PageModel
    {
        private readonly string _publicKey;
        private readonly string _privateKey;
        public required Comic Comic { get; set; }

        public AboutModel(IConfiguration configuration)
        {
            _publicKey = configuration["ApiInfo:PublicKey"] ?? "";
            _privateKey = configuration["ApiInfo:PrivateKey"] ?? "";
        }

        public async Task OnGet(int id)
        {
            var result = await GetComic(id);

            if (result != null)
                Comic = result;
            else
                RedirectToPage("/Error");
        }

        public async Task<Comic?> GetComic(int id)
        {
            string timeStamp = DateTime.Now.Ticks.ToString();
            string hash = ApiRequestHelper.GenerateApiHash(timeStamp, _privateKey, _publicKey);
            string apiUrl = $"http://gateway.marvel.com/v1/public/comics/{id}?ts={timeStamp}&apikey={_publicKey}&hash={hash}";
            
            return await ApiRequestHelper.GetComic(apiUrl);
        }
    }
}
