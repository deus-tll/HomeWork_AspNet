namespace Library.Models
{
    public class Cover
    {
        public string CoverPath { get; set; }
        public Cover(string path, string extension)
        {
            CoverPath = $"{path}.{extension}";
        }
    }
}
