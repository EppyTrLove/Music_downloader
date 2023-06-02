
using System.Text.Json;

namespace Music_downloader
{
    public class PostModelDao
    {
        private readonly string _dataBasePath;
        public PostModelDao(string dataBasePath)
        {
            _dataBasePath = dataBasePath;
            if (!File.Exists(_dataBasePath))
                File.Create(_dataBasePath);
        }
        public PostModel[] GetAll()
        {
            return File.ReadAllLines(_dataBasePath)
                .Select(x => JsonSerializer.Deserialize<PostModel>(x))
                .ToArray()!;
        }
        public void Add(PostModel[] models)
        {
            foreach (var model in models)
                File.AppendAllText(_dataBasePath, $"{JsonSerializer.Serialize(model)}\n");
        }
        public void RemoveAll()
        {
            File.Delete(_dataBasePath);
            File.Create(_dataBasePath).Dispose();
        }
    }
}
