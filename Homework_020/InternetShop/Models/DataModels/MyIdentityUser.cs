using Microsoft.AspNetCore.Identity;

namespace InternetShop.Models.DataModels
{
    public class MyIdentityUser : IdentityUser
    {
        public int Age { get; set; }
    }
}
