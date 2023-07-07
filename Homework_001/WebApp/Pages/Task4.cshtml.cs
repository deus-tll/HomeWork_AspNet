using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace WebApp.Pages
{
    public class Task4Model : PageModel
    {
        private readonly IConfiguration CONFIGURATION;
        public Task4Model(IConfiguration configuration)
        {
            CONFIGURATION = configuration;
        }

        public MyInfo MyInfo { get; set; }
        
        public void OnGet()
        {
            MyInfo = new MyInfo
            {
                FullName = CONFIGURATION["MyInfo:FullName"],
                Age = CONFIGURATION["MyInfo:Age"],
                Bio = CONFIGURATION["MyInfo:Bio"],
                Github = CONFIGURATION["MyInfo:Github"],
            };
        }
    }

    public class MyInfo
    {
        public string FullName { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Github { get; set; } = string.Empty;
    }
}
