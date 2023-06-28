
using System.Text.Json;
using static Music_downloader.PostModel;

namespace Music_downloader
{
    public class PostDao
    {
        private readonly string _dataBasePath;
        public PostDao(string dataBasePath)
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
    public class PostPeoplelDao
    {
        private readonly string _dataBasePath;
        public PostPeoplelDao(string dataBasePath)
        {
            _dataBasePath = dataBasePath;
            if (!File.Exists(_dataBasePath))
                File.Create(_dataBasePath);
        }
        public PostPeople[] GetAll()
        {
            return File.ReadAllLines(_dataBasePath)
                .Select(x => JsonSerializer.Deserialize<PostPeople>(x))
                .ToArray()!;
        }
        public void Add(PostPeople[] models)
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
