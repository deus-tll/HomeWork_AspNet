using InternetShop.Models.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Models.ViewModels
{
    public class IndexViewModel
    {
        public required List<Product> Products { get; set; }

        public required ProductFilter Filter { get; set; }
    }
}