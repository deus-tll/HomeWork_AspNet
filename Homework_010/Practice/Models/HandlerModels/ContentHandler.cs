using Practice.Models.DataModels;
using Practice.Models.ViewModels;

namespace Practice.Models.HandlerModels
{
    public class ContentHandler
    {
        private readonly JsonFileManager<ContentBlock> _contentFileManager;
        private readonly JsonFileManager<RefBlock> _refFileManager;
        private readonly JsonFileManager<MakeReviewViewModel> _messageFileManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public class NecessaryFiles
        {
            public const string INDEX_CONTENT_PATH = "IndexContent.json";
            public const string ABOUT_CONTENT_PATH = "AboutContent.json";
            public const string ADVANTAGES_CONTENT_PATH = "AdvantagesContent.json";
            public const string AREAS_CONTENT_PATH = "AreasContent.json";
            public const string APPS_CONTENT_PATH = "Apps.json";
            public const string MESSAGES_PATH = "Messages.json";
        }

        public ContentHandler(IWebHostEnvironment hostingEnvironment)
        {
            _contentFileManager = new JsonFileManager<ContentBlock>(hostingEnvironment);
            _refFileManager = new JsonFileManager<RefBlock>(hostingEnvironment);
            _messageFileManager = new JsonFileManager<MakeReviewViewModel>(hostingEnvironment);
            _hostingEnvironment = hostingEnvironment;

            InitializeFilesIfEmpty();
        }

        private void InitializeFilesIfEmpty()
        {
            string contentFilesPath = Path.Combine(_hostingEnvironment.WebRootPath, "ContentFiles");

            InitializeIfEmpty(contentFilesPath, NecessaryFiles.INDEX_CONTENT_PATH, _contentFileManager, HandlerModels.InitializeContent.InitializeIndexContent);
            InitializeIfEmpty(contentFilesPath, NecessaryFiles.ABOUT_CONTENT_PATH, _contentFileManager, HandlerModels.InitializeContent.InitializeAboutContent);
            InitializeIfEmpty(contentFilesPath, NecessaryFiles.ADVANTAGES_CONTENT_PATH, _contentFileManager, HandlerModels.InitializeContent.InitializeAdvantagesContent);
            InitializeIfEmpty(contentFilesPath, NecessaryFiles.AREAS_CONTENT_PATH, _contentFileManager, HandlerModels.InitializeContent.InitializeAreasContent);
            InitializeIfEmpty(contentFilesPath, NecessaryFiles.APPS_CONTENT_PATH, _refFileManager, HandlerModels.InitializeContent.InitializeAppsRefsList);
        }

        public void SaveMessageToFile(MakeReviewViewModel viewModel)
        {
            string filePath = _messageFileManager.GetFilePath(NecessaryFiles.MESSAGES_PATH, "Messages");

            List<MakeReviewViewModel>? messages = _messageFileManager.ReadFromFile(filePath) ?? new List<MakeReviewViewModel>();

            messages.Add(viewModel);

            _messageFileManager.SaveToFile(filePath, messages);
        }

        public List<MakeReviewViewModel>? ReadMessagesFromFile()
        {
            string filePath = _messageFileManager.GetFilePath(NecessaryFiles.MESSAGES_PATH, "Messages");
            return _messageFileManager.ReadFromFile(filePath);
        }

        public List<ContentBlock>? GetContentBlocks(string contentFilePath)
        {
            string contentFilesPath = Path.Combine(_hostingEnvironment.WebRootPath, "ContentFiles");
            string filePath = _contentFileManager.GetFilePath(contentFilePath, contentFilesPath);

            return _contentFileManager.ReadFromFile(filePath);
        }

        public List<RefBlock>? GetRefBlocks(string contentFilePath)
        {
            string contentFilesPath = Path.Combine(_hostingEnvironment.WebRootPath, "ContentFiles");
            string filePath = _refFileManager.GetFilePath(contentFilePath, contentFilesPath);

            return _refFileManager.ReadFromFile(filePath);
        }


        private static void InitializeIfEmpty<T>(string contentFilesPath, string contentFilePath, JsonFileManager<T> fileManager, Func<List<T>> initializeFunc)
        {
            string filePath = fileManager.GetFilePath(contentFilePath, contentFilesPath);

            List<T>? items = fileManager.ReadFromFile(filePath);

            if (items is null)
            {
                items = initializeFunc();
                fileManager.SaveToFile(filePath, items);
            }
        }
    }
}