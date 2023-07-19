using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Extensions;

namespace Portfolio.Pages
{
    public class CoursesModel : PageModel
    {
        public List<string> Files { get; set; } = new();
        public void OnGet()
        {
            try
            {
                string dirAfterRoot = Path.Combine("img", "courses");
                string dir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", dirAfterRoot);
                string[]? files = Extension.GetFilePaths(dir);

                if (files is not null)
                {
                    foreach (string file in files)
                    {
                        Files.Add($"/{dirAfterRoot}/{Path.GetFileName(file)}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
