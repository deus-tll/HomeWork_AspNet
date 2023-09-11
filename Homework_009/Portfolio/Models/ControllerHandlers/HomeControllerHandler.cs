using Portfolio.Controllers;
using Portfolio.Models.DataModels;

namespace Portfolio.Models.ControllerHandlers
{
    public class HomeControllerHandler
    {
        private readonly ILogger<HomeController> _logger;


        public HomeControllerHandler(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        private async Task<string[]?> GetFilePathsAsync(string dir)
        {
            try
            {
                return await Task.Run(() => Directory.GetFiles(dir));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while getting files.");
                return null;
            }
        }


        public async Task<List<string>> GetFilePathsAsync()
        {
            List<string> list = new();

            try
            {
                string dirAfterRoot = Path.Combine("img", "courses");
                string dir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", dirAfterRoot);
                string[]? files = await GetFilePathsAsync(dir);

                if (files is not null)
                {
                    foreach (string file in files)
                    {
                        list.Add($"/{dirAfterRoot}/{Path.GetFileName(file)}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while preparing files.");
            }

            return list;
        }


        public async Task<List<MyFile>> GetFilesAsync()
        {
            List<MyFile> list = new();

            try
            {
                string dirAfterRoot = Path.Combine("img", "portfolio");
                string dir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", dirAfterRoot);
                string[]? files = await GetFilePathsAsync(dir);

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

                        list.Add(myFile);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while preparing files.");
            }

            return list;
        }
    }
}
