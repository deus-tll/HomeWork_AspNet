namespace Portfolio.Extensions
{
    public static class Extension
    {
        public static string[]? GetFilePaths(string dir)
        {
			try
			{
                return Directory.GetFiles(dir);
            }
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
