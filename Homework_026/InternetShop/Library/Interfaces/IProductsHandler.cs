using Library.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interfaces
{
    public interface IProductsHandler
    {
        Task<List<ProductDb>> GetTrendyProducts(int firstRange = 3, int secondRange = 6, int amount = 20);
        Task<ProductDb?> GetProductById(int productId);
        Task AddCartItem(CartItem cartItem);
        Task<int> GetCountProductsInCart(string userId);
        Task<List<Category>> GetCategories();
        Task<List<Brand>> GetBrands();
        Task<List<ProductDb>> GetProducts();
        Task<int> GetCountProductsByBrand(int productId);
        Task<int> GetCountProductsByCategory(int productId);
    }
}
