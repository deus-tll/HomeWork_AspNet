using InternetShop.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Models.HandlerModels
{
    public interface IProductHandler
    {
        Task<List<Product>> GetProductsAsync();
        Task<List<Product>> GetFilteredProductsAsync(ProductFilter? filter);
        Task<Product?> GetProductAsync(int id);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task AddProductAsync(Product product);
    }

    public class ProductHandler : IProductHandler
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ProductHandler> _logger;

        public ProductHandler(ApplicationContext context, ILogger<ProductHandler> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<List<Product>> GetProductsAsync() => await _context.Products.AsNoTracking().ToListAsync();


        public async Task<List<Product>> GetFilteredProductsAsync(ProductFilter? filter)
        {
            List<Product> products = await GetProductsAsync();

            if (filter is not null)
            {
                products = products
                .Where(p => (string.IsNullOrEmpty(filter.Name) || p.Name.ToLower().Contains(filter.Name.ToLower())) &&
                            (string.IsNullOrEmpty(filter.Category) || p.Category.ToLower().Contains(filter.Category.ToLower())) &&
                            (string.IsNullOrEmpty(filter.Brand) || p.Brand.ToLower().Contains(filter.Brand.ToLower())) &&
                            (filter.MinPrice is null || p.Price >= filter.MinPrice) &&
                            (filter.MaxPrice is null || p.Price <= filter.MaxPrice) &&
                            (filter.MinStockQuantity is null || p.StockQuantity >= filter.MinStockQuantity) &&
                            (filter.MaxStockQuantity is null || p.StockQuantity <= filter.MaxStockQuantity) &&
                            (filter.MinYearOfManufacture is null || p.YearOfManufacture >= filter.MinYearOfManufacture) &&
                            (filter.MaxYearOfManufacture is null || p.YearOfManufacture <= filter.MaxYearOfManufacture))
                .ToList();

                products = filter.SortBy switch
                {
                    "Name" => filter.IsSortAscending ? products.OrderBy(p => p.Name).ToList() : products.OrderByDescending(p => p.Name).ToList(),
                    "Category" => filter.IsSortAscending ? products.OrderBy(p => p.Category).ToList() : products.OrderByDescending(p => p.Category).ToList(),
                    "Brand" => filter.IsSortAscending ? products.OrderBy(p => p.Brand).ToList() : products.OrderByDescending(p => p.Brand).ToList(),
                    _ => products
                };
            }
            
            return products;
        }


        public async Task<Product?> GetProductAsync(int id) => await _context.Products.FindAsync(id);


        public async Task UpdateProductAsync(Product product)
        {            
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while performing \"Update element in Products\" operation in the DB.");
            }
        }


        public async Task DeleteProductAsync(Product product)
        {
            try
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while performing \"Remove element from Products\" operation in the DB.");
            }
        }


        public async Task AddProductAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while performing \"Add element to Products\" operation in the DB.");
            }
        }
    }
}