using Library.Interfaces;
using Library.Models.ContextModels;
using Library.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.HandlerModels
{
    public class ProductsHandler : IProductsHandler
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductsHandler> _logger;

        public ProductsHandler(ApplicationDbContext context, ILogger<ProductsHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ProductDb>> GetTrendyProducts(int firstRange = 3, int secondRange = 6, int amount = 20)
        {
            DateTime currentTime = DateTime.Now;

            DateTime firstPeriodStart = currentTime.AddMonths(-firstRange);
            DateTime secondPeriodStart = currentTime.AddMonths(-secondRange);

            var trendingProducts = await _context.Products
                .Where(p => p.DateAdded >= firstPeriodStart)
                .OrderByDescending(p => p.Orders)
                .Take(amount)
                .ToListAsync();

            if (trendingProducts.Count < amount)
            {
                var additionalProducts = await _context.Products
                    .Where(p => p.DateAdded >= secondPeriodStart && p.DateAdded < firstPeriodStart)
                    .OrderByDescending (p => p.Orders)
                    .Take (amount - trendingProducts.Count)
                    .ToListAsync();

                trendingProducts.AddRange(additionalProducts);
            } 

            return trendingProducts;
        }

        public async Task<ProductDb?> GetProductById(int productId) => await _context.Products.FindAsync(productId);

        public async Task AddCartItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountProductsInCart(string userId)
        {
            int cartCount = await _context.CartItems
                .Where(ci => ci.UserId == userId)
                .SumAsync(ci => ci.Quantity);

            return cartCount;
        }

        public async Task<List<Category>> GetCategories() => await _context.Categories.ToListAsync();

        public async Task<List<ProductDb>> GetProducts() => await _context.Products.ToListAsync();

        public async Task<List<Brand>> GetBrands() => await _context.Brands.ToListAsync();

        public async Task<int> GetCountProductsByBrand(int productId)
        {
            int productCount = await _context.Products
            .CountAsync(p => p.BrandId == productId);

            return productCount;
        }

        public async Task<int> GetCountProductsByCategory(int productId)
        {
            int productCount = await _context.Products
            .CountAsync(p => p.CategoryId == productId);

            return productCount;
        }
    }
}
