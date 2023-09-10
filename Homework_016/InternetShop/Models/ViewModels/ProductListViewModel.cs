using InternetShop.Models.DataModels;

namespace InternetShop.Models.ViewModels
{
    public class ProductListViewModel
    {
        public required PaginatedList<Product> PaginatedList { get; set; }
    }
}
