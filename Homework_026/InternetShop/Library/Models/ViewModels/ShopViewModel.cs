using Library.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.ViewModels
{
    public class ShopViewModel
    {
        public required List<ProductDb> Products { get; set; }
        public required List<DisplayCategory> Categories { get; set; }
        public required List<DisplayBrand> Brands { get; set; }

        public int AllProductsInCategories { get; set; }
        public int AllProductsInBrands { get; set; }
    }
}
