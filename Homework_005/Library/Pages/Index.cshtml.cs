using Library.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Library.Pages
{
    public class IndexModel : PageModel
    {
        private const string WWWWROOT = "wwwroot";
        private const string DATA_FOLDER = "data";
        private const string DATA_FETCHED_FILE = "fetchedData.json";
        private readonly string _publicKey;
        private readonly string _privateKey;
        private readonly string _lastFetchDate;
        private readonly int _limit = 0;
        public required List<Comic>? Comics { get; set; }
        public IndexModel(IConfiguration configuration)
        {
            _publicKey = configuration["ApiInfo:PublicKey"] ?? "";
            _privateKey = configuration["ApiInfo:PrivateKey"] ?? "";
            _lastFetchDate = configuration["ApiInfo:LastFetchDate"] ?? "";
            _ = int.TryParse(configuration["ApiInfo:Limit"], out _limit);
        }


        public async Task OnGet()
        {
            DateTime now = DateTime.Now;

            if (string.IsNullOrEmpty(_lastFetchDate) || (now - DateTime.Parse(_lastFetchDate)).TotalDays >= 1)
            {
                var result = await GetComics();

                if (result is null)
                {
                    RedirectToPage("/Error");
                    return;
                }

                Comics = result;
                await SaveComicsToJsonFile(Comics);

                string path = "appsettings.json";

                var jsonData = await System.IO.File.ReadAllTextAsync(path);
                dynamic? appSettings = DeserializeDynamic(jsonData);

                if (appSettings is not null)
                {
                    string lastFetchDateString = now.ToString("yyyy-MM-ddTHH:mm:ssZ");
                    appSettings.ApiInfo.LastFetchDate = lastFetchDateString;

                    System.IO.File.WriteAllText(path, SerializeDynamic(appSettings));
                }
            }
            else
            {
                var filePath = Path.Combine(WWWWROOT, DATA_FOLDER, DATA_FETCHED_FILE);
                var jsonData = await System.IO.File.ReadAllTextAsync(filePath);
                Comics = JsonConvert.DeserializeObject<List<Comic>>(jsonData);
            }
        }


        public async Task<List<Comic>?> GetComics()
        {
            string timeStamp = DateTime.Now.Ticks.ToString();
            string hash = ApiRequestHelper.GenerateApiHash(timeStamp, _privateKey, _publicKey);
            string apiUrl = $"http://gateway.marvel.com/v1/public/comics?ts={timeStamp}&apikey={_publicKey}&hash={hash}&limit={IsLimit()}";

            return await ApiRequestHelper.GetComics(apiUrl);
        }


        private static async Task SaveComicsToJsonFile(List<Comic> comics)
        {
            var jsonData = System.Text.Json.JsonSerializer.Serialize(comics);

            var directoryPath = Path.Combine(WWWWROOT, DATA_FOLDER);
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(WWWWROOT, DATA_FOLDER, DATA_FETCHED_FILE);
            await System.IO.File.WriteAllTextAsync(filePath, jsonData);
        }


        private static dynamic? DeserializeDynamic(string json)
        {
            return JsonConvert.DeserializeObject<dynamic>(json);
        }


        private static string SerializeDynamic(dynamic obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }


        private string IsLimit()
        {
            if (_limit == 0)
                return "";
            else
                return _limit.ToString();
        }
    }
}