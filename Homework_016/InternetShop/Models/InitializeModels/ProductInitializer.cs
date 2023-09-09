using InternetShop.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Models.InitializeModels
{
    public class ProductInitializer
    {
        private readonly ApplicationContext _context;

        public ProductInitializer(ApplicationContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            await SeedProductsAsync();
        }

        private async Task SeedProductsAsync()
        {
            List<Product> products = await _context.Products.AsNoTracking().ToListAsync();

            if (!products.Any())
            {
                await _context.AddRangeAsync(CreateListOfDefaultProducts());
                await _context.SaveChangesAsync();
            }
        }

        private static List<Product> CreateListOfDefaultProducts()
        {
            List<Product> products = new()
            {
                new Product
                {
                    Name = "ProductName1",
                    Description = "ProductDescription1",
                    Category = "ProductCategory1",
                    Brand = "ProductBrand1",
                    Price = 1111,
                    StockQuantity = 1,
                    YearOfManufacture = 2001
                },
                new Product
                {
                    Name = "ProductName2",
                    Description = "ProductDescription2",
                    Category = "ProductCategory2",
                    Brand = "ProductBrand2",
                    Price = 2222,
                    StockQuantity = 2,
                    YearOfManufacture = 2002
                },
                new Product
                {
                    Name = "ProductName3",
                    Description = "ProductDescription3",
                    Category = "ProductCategory3",
                    Brand = "ProductBrand3",
                    Price = 3333,
                    StockQuantity = 3,
                    YearOfManufacture = 2003
                },
                new Product
                {
                    Name = "ProductName4",
                    Description = "ProductDescription4",
                    Category = "ProductCategory4",
                    Brand = "ProductBrand4",
                    Price = 4444,
                    StockQuantity = 4,
                    YearOfManufacture = 2004
                },
                new Product
                {
                    Name = "ProductName5",
                    Description = "ProductDescription5",
                    Category = "ProductCategory5",
                    Brand = "ProductBrand5",
                    Price = 5555,
                    StockQuantity = 5,
                    YearOfManufacture = 2005
                },
                new Product
                {
                    Name = "ProductName6",
                    Description = "ProductDescription6",
                    Category = "ProductCategory6",
                    Brand = "ProductBrand6",
                    Price = 6666,
                    StockQuantity = 6,
                    YearOfManufacture = 2006
                },
                new Product
                {
                    Name = "ProductName7",
                    Description = "ProductDescription7",
                    Category = "ProductCategory7",
                    Brand = "ProductBrand7",
                    Price = 7777,
                    StockQuantity = 7,
                    YearOfManufacture = 2007
                },
                new Product
                {
                    Name = "ProductName8",
                    Description = "ProductDescription8",
                    Category = "ProductCategory8",
                    Brand = "ProductBrand8",
                    Price = 8888,
                    StockQuantity = 8,
                    YearOfManufacture = 2008
                },
                new Product
                {
                    Name = "ProductName9",
                    Description = "ProductDescription9",
                    Category = "ProductCategory9",
                    Brand = "ProductBrand9",
                    Price = 9999,
                    StockQuantity = 9,
                    YearOfManufacture = 2009
                },
                new Product
                {
                    Name = "ProductName10",
                    Description = "ProductDescription10",
                    Category = "ProductCategory10",
                    Brand = "ProductBrand10",
                    Price = 10000,
                    StockQuantity = 10,
                    YearOfManufacture = 2010
                },
            };

            return products;
        }
    }
}
