using Newtonsoft.Json;

namespace AboutCSharp.Models.HandlerModels
{
    public class JsonFileManager<T>
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public JsonFileManager(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string GetFilePath(string fileName, string folderName)
        {
            string filesPath = Path.Combine(_hostingEnvironment.WebRootPath, folderName);
            return Path.Combine(filesPath, fileName);
        }

        public List<T>? ReadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            using StreamReader fileReader = File.OpenText(filePath);
            using JsonReader jsonReader = new JsonTextReader(fileReader);
            JsonSerializer jsonSerializer = new();

            return jsonSerializer.Deserialize<List<T>>(jsonReader);
        }

        public void SaveToFile(string filePath, List<T>? data)
        {
            if (data is null) return;

            using StreamWriter file = File.CreateText(filePath);
            JsonSerializer jsonSerializer = new();
            jsonSerializer.Serialize(file, data);
        }
    }
}
