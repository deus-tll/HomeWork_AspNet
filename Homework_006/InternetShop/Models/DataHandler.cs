using InternetShop.Pages.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Reflection.Metadata.BlobBuilder;

namespace InternetShop.Models
{
    public class DataHandler
    {
        private readonly UserManager<IdentityUser>? _userManager;
        private readonly SignInManager<IdentityUser>? _signInManager;
        private readonly ApplicationContext? _context;


        public DataHandler(UserManager<IdentityUser> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public DataHandler(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public DataHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public DataHandler(ApplicationContext context)
        {
            _context = context;
        }


        public async Task<List<Message>> GetMessages()
        {
            if (_context is null)
                return new();
            else
                return await _context.Messages.ToListAsync();
        }


        public async Task<string?> GetUserEmail(string userId)
        {
            if (_userManager == null) return null;
            var user = await _userManager.FindByIdAsync(userId);
            return user?.Email;
        }


        public async Task<string> SendMessageToManager(ClaimsPrincipal currentUser, string messageText)
        {
            if (_userManager == null) return "/Error";
            var user = await _userManager.GetUserAsync(currentUser);
            if (user == null) return "/Error";

            var message = new Models.Message
            {
                Text = messageText,
                CreatedAt = DateTime.Now,
                UserId = user.Id
            };

            if (_context is null)
                return "/Error";

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return "/Index";
        }


        private async Task<List<Product>> GetProductsFromDbAsync()
        {
            if (_context is null)
                return new();
            else
                return await _context.Products.AsNoTracking().ToListAsync();
        }


        public async Task<List<Product>> GetProductsAsync()
        {
            List<Product> products = await GetProductsFromDbAsync();

            if (!products.Any())
            {
                if (_context is not null)
                {
                    await _context.AddRangeAsync(Extension.CreateListOfDefaultProducts());
                    await _context.SaveChangesAsync();
                    products = await GetProductsFromDbAsync();
                }
            }

            return products;
        }

        public async Task<List<Product>> GetFilteredProductsAsync(ProductFilter filter)
        {
            List<Product> products = await GetProductsFromDbAsync();

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

            return products;
        }

        public async Task<Product?> GetProductAsync(int id)
        {
            if (_context == null) return null;
            return await _context.Products.FindAsync(id);
        }

        public async Task UpdateProductAsync(Product product)
        {
            if (_context is null) return;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            if (_context is null) return;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            if (_context is null) return;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
    }
}
