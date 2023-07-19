using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Extensions;

namespace Portfolio.Pages
{
    public class PortfolioModel : PageModel
    {
        public List<MyFile> Files { get; set; } = new();
        public void OnGet()
        {
            GetFiles();
        }

        private void GetFiles()
        {
            try
            {
                string dirAfterRoot = Path.Combine("img", "portfolio");
                string dir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", dirAfterRoot);
                string[]? files = Extension.GetFilePaths(dir);

                if (files is not null)
                {
                    foreach (string file in files)
                    {
                        MyFile myFile = new()
                        {
                            Name = Path.GetFileNameWithoutExtension(file),
                            Path = $"\\{dirAfterRoot}\\{Path.GetFileName(file)}",
                            Ext = Path.GetExtension(file)
                        };

                        int indexOfTargetChar = myFile.Name.IndexOf('_');
                        myFile.Type = myFile.Name[..indexOfTargetChar];

                        Files.Add(myFile);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public class MyFile
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Ext { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
