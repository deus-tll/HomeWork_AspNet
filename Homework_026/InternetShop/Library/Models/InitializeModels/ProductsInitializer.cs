using Library.Models.ContextModels;
using Library.Models.DataModels;
using Library.Models.HandlerModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Library.Models.InitializeModels
{
    public class ProductsInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly string _url;
        private readonly ILogger<ProductsInitializer> _logger;
        private readonly static Random _random = new();


        public ProductsInitializer(ApplicationDbContext context, string url, ILogger<ProductsInitializer> logger)
        {
            _context = context;
            _url = url;
            _logger = logger;
        }

        private class ResponseObject
        {
            public required List<ProductJson> Products { get; set; }
        }

        public async Task InitializeAsync() => await SeedProductsAsync();


        private async Task SeedProductsAsync()
        {
            List<ProductDb> dbProducts = await _context.Products.AsNoTracking().ToListAsync();

            if (!dbProducts.Any())
            {
                List<ProductJson> jsonProducts = await GetProductsFromUrl(_url);
                dbProducts.AddRange(await ConvertProductsJsonToDb(jsonProducts));

                await _context.Products.AddRangeAsync(dbProducts);
                await _context.SaveChangesAsync();
            }
        }


        private async Task<List<ProductJson>> GetProductsFromUrl(string url)
        {
            using HttpClient httpClient = new();
            try
            {
                string json = await httpClient.GetStringAsync(url);

                var response = JsonConvert.DeserializeObject<ResponseObject>(json);

                if (response?.Products is null)
                    throw new NullReferenceException("Invalid JSON format or empty products list");

                return response.Products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while performing reading or converting products from json to db.");
            }

            return new();
        }


        private async Task<List<ProductDb>> ConvertProductsJsonToDb(List<ProductJson> jsonProducts)
        {
            List<ProductDb> dbProducts = new();

            Dictionary<string, Brand> brandDictionary = new();
            Dictionary<string, Category> categoryDictionary = new();

            foreach (var product in jsonProducts)
            {
                Brand brand;
                Category category;

                string brandName = product.Brand;
                if (!brandDictionary.ContainsKey(brandName))
                {
                    brand = new Brand { Name = brandName };
                    await _context.Brands.AddAsync(brand);
                    brandDictionary.Add(brandName, brand);
                }

                string categoryStr = NormalizeCategory(product.Category);
                if (!categoryDictionary.ContainsKey(categoryStr))
                {
                    category = new Category { Name = categoryStr };
                    await _context.Categories.AddAsync(category);
                    categoryDictionary.Add(categoryStr, category);
                }

                brand = brandDictionary[brandName];
                category = categoryDictionary[categoryStr];

                DateTime currentDate = DateTime.Now;

                ProductDb productDb = new()
                {
                    Title = product.Title,
                    Description = product.Description,
                    Price = product.Price,
                    DiscountPercentage = product.DiscountPercentage,
                    Rating = product.Rating,
                    Stock = product.Stock,
                    Views = GetRandomInt(0, 5000),
                    Orders = GetRandomInt(0, 1500),
                    DateAdded = RandomizeDate(currentDate.AddYears(-1), currentDate),
                    Brand = brand,
                    Category = category,
                    Thumbnail = product.Thumbnail
                };
                

                if (product.Images is null || product.Images.Count is 0)
                {
                    dbProducts.Add(productDb);
                    continue;
                }

                productDb.Images = new();

                foreach (var imageUrl in product.Images)
                {
                    productDb.Images.Add(new ProductImage()
                    {
                        Url = imageUrl,
                    });
                }

                dbProducts.Add(productDb);
            }

            return dbProducts;
        }


        private static string NormalizeCategory(string input)
        {
            string[] words = input.Split('-');

            if (words.Length > 1)
                return string.Join(" ", words.Select(w => CapitalizeString(w)));

            if (!string.IsNullOrWhiteSpace(input))
                return CapitalizeString(input);

            return input;
        }


        private static string CapitalizeString(string input) => char.ToUpper(input[0]) + input[1..];


        private static int GetRandomInt(int rangeMin, int rangeMax)
        {
            return _random.Next(rangeMin, rangeMax);
        }


        public static DateTime RandomizeDate(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
            {
                throw new ArgumentException("startDate must be earlier than endDate");
            }

            int range = (endDate - startDate).Days;

            int randomDay = _random.Next(range);

            DateTime randomDate = startDate.AddDays(randomDay);

            return randomDate;
        }
    }
}
