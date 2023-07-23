using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Library.Models
{
    public class ApiRequestHelper
    {
        public static string GenerateApiHash(string timeStamp, string privateKey, string publicKey)
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


        private static async Task<dynamic?> GetJsonDataAsync(string apiUrl)
        {
            using var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode) return null;
            string content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<dynamic>(content); ;
        }


        public static async Task<Comic?> GetComic(string apiUrl)
        {
            dynamic? jsonData = await GetJsonDataAsync(apiUrl);
            if (jsonData == null) return null;

            return GetComicFromDynamic(jsonData.data.results[0]);
        }


        public static async Task<List<Comic>?> GetComics(string apiUrl)
        {
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
    }
}
