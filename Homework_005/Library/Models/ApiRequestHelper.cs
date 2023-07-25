using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Library.Models
{
    public class ApiRequestHelper : IDisposable
    {
        /// <summary>
        /// If this string changes, you should also change the name of property of dynamic object
        /// appSettings in method DefineData()
        /// <para>
        /// aproppriate line -<br/>
        /// appSettings.ApiInfoSettings.LastFetchDate = lastFetchDateString;<br/>
        /// appSettings.(change to whatever in here - API_INFO_OBJECT_NAME).LastFetchDate = lastFetchDateString;
        /// </para>
        /// 
        /// </summary>
        private readonly string API_INFO_OBJECT_NAME = "ApiInfoSettings";

        private readonly string _wwwroot;
        private readonly string _dataFolder;
        private readonly string _dataFetchedFile;
        private readonly string _lastFetchDate;

        private readonly string _publicKey;
        private readonly string _privateKey;
        private readonly int _limit = 0;
        private readonly int _daysCountForUpdate = 1;

        private readonly HttpClient _httpClient;
        public ApiRequestHelper(IConfiguration configuration)
        {
            _wwwroot = configuration[$"{API_INFO_OBJECT_NAME}:Wwwroot"] ?? "";
            _dataFolder = configuration[$"{API_INFO_OBJECT_NAME}:DataFolder"] ?? "";
            _dataFetchedFile = configuration[$"{API_INFO_OBJECT_NAME}:DataFetchedFile"] ?? "";
            _publicKey = configuration[$"{API_INFO_OBJECT_NAME}:PublicKey"] ?? "";
            _privateKey = configuration[$"{API_INFO_OBJECT_NAME}:PrivateKey"] ?? "";
            _lastFetchDate = configuration[$"{API_INFO_OBJECT_NAME}:LastFetchDate"] ?? "";
            _ = int.TryParse(configuration[$"{API_INFO_OBJECT_NAME}:Limit"], out _limit);
            _ = int.TryParse(configuration[$"{API_INFO_OBJECT_NAME}:DaysCountForUpdate"], out _daysCountForUpdate);
            _httpClient = new HttpClient();
        }


        private static string GenerateApiHash(string timeStamp, string privateKey, string publicKey)
        {
            string preHashString = $"{timeStamp}{privateKey}{publicKey}";
            byte[] bytes = Encoding.UTF8.GetBytes(preHashString);
            byte[] hashBytes = MD5.HashData(bytes);

            StringBuilder sb = new();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }


        private async Task<dynamic?> GetJsonDataAsync(string apiUrl)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode) return null;
            string content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<dynamic>(content); ;
        }


        public async Task<Comic?> GetComic(int id)
        {
            List<Comic>? cachedComics = await GetCachedComics();
            Comic? comic = cachedComics?.FirstOrDefault(c => c.Id == id);

            if (comic != null) return comic;

            string timeStamp = DateTime.Now.Ticks.ToString();
            string hash = ApiRequestHelper.GenerateApiHash(timeStamp, _privateKey, _publicKey);
            string apiUrl = $"http://gateway.marvel.com/v1/public/comics/{id}?ts={timeStamp}&apikey={_publicKey}&hash={hash}";

            dynamic? jsonData = await GetJsonDataAsync(apiUrl);
            if (jsonData == null) return null;

            return GetComicFromDynamic(jsonData.data.results[0]);
        }


        public async Task<List<Comic>?> GetComics()
        {
            string timeStamp = DateTime.Now.Ticks.ToString();
            string hash = ApiRequestHelper.GenerateApiHash(timeStamp, _privateKey, _publicKey);
            string apiUrl = $"http://gateway.marvel.com/v1/public/comics?ts={timeStamp}&apikey={_publicKey}&hash={hash}&limit={IsLimit()}";

            dynamic? jsonData = await GetJsonDataAsync(apiUrl);

            if (jsonData == null) return null;

            List<Comic> comics = new();
            foreach (var comic in jsonData.data.results)
            {
                Comic newComic = GetComicFromDynamic(comic);
                comics.Add(newComic);
            }

            return comics;
        }


        private static Comic GetComicFromDynamic(dynamic comic)
        {
            string onSaleDateString = string.Empty;
            foreach (var date in comic.dates)
            {
                if (date.type == "onsaleDate")
                {
                    onSaleDateString = date.date;
                    break;
                }
            }

            List<Price> prices = new();
            foreach(var price in comic.prices)
            {
                string priceTypeString = price.type;
                decimal priceValue = price.price;

                StringBuilder sb = new();
                sb.Append(char.ToUpper(priceTypeString[0]));

                for (int i = 1; i < priceTypeString.Length; i++)
                {
                    if (char.IsUpper(priceTypeString[i]))
                    {
                        sb.Append(' ');
                    }
                    sb.Append(priceTypeString[i]);
                }

                Price newPrice = new()
                {
                    Value = priceValue,
                    Type = sb.ToString()
                };

                prices.Add(newPrice);
            }

            Cover cover = new((string)comic.thumbnail.path, (string)comic.thumbnail.extension);
            List<Creator> creators = new();

            foreach (var creator in comic.creators.items)
            {
                Creator newCreator = new() { Name = creator.name, Role = creator.role };
                creators.Add(newCreator);
            }

            Comic newComic = new()
            {
                Id = comic.id,
                Title = comic.title,
                Description = comic.description,
                Format = comic.format,
                OnSaleDate = DateTime.Parse(onSaleDateString),
                PageCount = (int)comic.pageCount,
                ComicCover = cover,
                Prices = prices,
                Creators = creators
            };

            return newComic;
        }


        private async Task SaveComicsToJsonFile(List<Comic> comics)
        {
            string? jsonData = JsonConvert.SerializeObject(comics);

            string? directoryPath = Path.Combine(_wwwroot, _dataFolder);
            Directory.CreateDirectory(directoryPath);

            string? filePath = Path.Combine(_wwwroot, _dataFolder, _dataFetchedFile);
            await File.WriteAllTextAsync(filePath, jsonData);
        }


        private static async Task<List<Comic>?> DeserializeComics(string filePath)
        {
            string? jsonData = await File.ReadAllTextAsync(filePath);
            return JsonConvert.DeserializeObject<List<Comic>>(jsonData);
        }


        public async Task<List<Comic>?> DefineData()
        {
            DateTime now = DateTime.Now;
            List<Comic>? comics;

            if (string.IsNullOrEmpty(_lastFetchDate) || (now - DateTime.Parse(_lastFetchDate)).TotalDays >= _daysCountForUpdate)
            {
                comics = await GetComics();

                if (comics is not null)
                {
                    await SaveComicsToJsonFile(comics);

                    string path = "appsettings.json";

                    string? jsonData = await File.ReadAllTextAsync(path);
                    dynamic? appSettings = ApiRequestHelper.DeserializeDynamic(jsonData);

                    if (appSettings is not null)
                    {
                        string lastFetchDateString = now.ToString("yyyy-MM-ddTHH:mm:ssZ");
                        appSettings.ApiInfoSettings.LastFetchDate = lastFetchDateString;

                        File.WriteAllText(path, ApiRequestHelper.SerializeDynamic(appSettings));
                    }
                }
            }
            else
            {
                comics = await GetCachedComics();
            }

            return comics;
        }

        public async Task<List<Comic>?> GetCachedComics()
        {
            string filePath = Path.Combine(_wwwroot, _dataFolder, _dataFetchedFile);
            return await DeserializeComics(filePath);
        }


        private static dynamic? DeserializeDynamic(string json) => JsonConvert.DeserializeObject<dynamic>(json);


        private static string SerializeDynamic(dynamic obj) => JsonConvert.SerializeObject(obj, Formatting.Indented);


        private string IsLimit()
        {
            if (_limit == 0)
                return "";
            else
                return _limit.ToString();
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
                _httpClient.Dispose();
            }
        }
    }
}