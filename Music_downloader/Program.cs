using Microsoft.Extensions.Configuration;
using Music_downloader;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

class Program {
    public static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var client = new VkApi();
        client.Authorize(new ApiAuthParams
        {
            Login = config["Login"],
            Password = config["Password"],
            AccessToken = config["VkToken"],
            Settings = Settings.All,
            ApplicationId = 51661636
        });
        Console.WriteLine(client.Token);

        var groups = client.Groups.GetById(new [] {"grimeofficial"}, null, GroupsFields.All).FirstOrDefault();
        var get = client.Wall.Get(new WallGetParams
        {
            OwnerId = -groups.Id,
            Offset = 18338,
        });
        var postModelDao = new PostModelDao($@"{Environment.CurrentDirectory}\Database.txt");
        var data = postModelDao.GetAll().ToList();
        var localData = new List<PostModel>();

        foreach (var post in get.WallPosts)
            if (post.Attachment.Type.FullName == "VkNet.Model.Attachments.Video" ||
                !data.Any(x => x.GetHashCode() == post.GetHashCode()))
                localData.Add(new PostModel(post., post.Likes.Count, post.Reposts.Count))
            else break;
    }
}

  




