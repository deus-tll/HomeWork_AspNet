using InternetShop.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InternetShop.Models.ViewModels
{
    public class IndexViewModel
    {
        public required ProductFilter Filter { get; set; }

        public required List<SelectListItem> SortByOptions { get; set; }

        public required PaginatedList<Product> PaginatedList { get; set; }

        public bool IsFiltered { get; set; }
    }
}