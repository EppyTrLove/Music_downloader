using Microsoft.Extensions.Configuration;
using Music_downloader;
using System.Diagnostics;
using System.Net.Mail;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;
using static Music_downloader.PostModel;

class Program
{
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
        Console.WriteLine("Helo my friend! Your VK Token =" + client.Token + "\nPlease enter name of the group you want " +
            "to scan:");
        var inputValue = "grimeofficial";
        var groups = client.Groups.GetById(new[] { inputValue }, null, GroupsFields.All).FirstOrDefault();

        var postDao = new PostDao($@"{Environment.CurrentDirectory}\MainDatabase.txt");
        var postData = postDao.GetAll().ToList();
        var PostPeopleDao = new PostPeoplelDao($@"{Environment.CurrentDirectory}\PostPeopleDatabase.txt");
        var PostPeopleData = PostPeopleDao.GetAll().ToList();

        ulong offset = 0;
        ulong getCount = 0;
        var posts = new List<PostModel>();
        var peoples = new List<PostPeople>();
        //var result = new HttpClient();
        do
        {
            try
            {
                var responce = client.Wall.Get(new WallGetParams
                {
                    OwnerId = -groups.Id,
                    Offset = offset,
                    Count = 100,
                });
                offset += 100;
                getCount = responce.TotalCount;

                foreach (var wallPost in responce.WallPosts)
                {
                    if (postData.Any(x => Equals(x.Id, wallPost.Id)))
                        continue;
                    var post = new PostModel(wallPost.Id!.Value, wallPost.Text, wallPost.Likes.Count,
                        wallPost.Reposts.Count, wallPost.Date.Value);
                    
                    var attachments = new List<AttachmentModel>();
                    foreach (var attachment in wallPost.Attachments)
                    {
                        if (attachment.Type.FullName == "VkNet.Model.Attachments.Document")
                        {
                            attachments.Add(new AttachmentModel { Link = (attachment.Instance as Document).Uri });
                        }
                    }
                    if (attachments.Any())
                    {
                        post.Attachments = attachments.ToArray();
                        posts.Add(post);
                    }
                    Console.WriteLine(offset);
                    var likersId = client.Likes.GetList(new LikesGetListParams
                    {
                        ItemId = wallPost.Id.Value,
                        //OwnerId = -groups.Id,
                    }).ToArray();
                    var people = new PostPeople(wallPost.Id!.Value, likersId);
                    peoples.Add(people);
                }            
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
            }
        }
        while ( offset % 100 == 0);
        postDao.Add(posts.ToArray());
        PostPeopleDao.Add(peoples.ToArray());




        //var result = new HttpClient()
        //.GetByteArrayAsync(
        //    "https://vk.com/doc65735415_298672804?hash=CzZfy0SeDEuY255lCngJO2sxZqWwlvThnngr15gXZcT&dl=f2P7ZxrU3aw23DMU41RXgowhDOECcVRmrBI9UnbygJs&api=1&no_preview=1")
        //    .Result;
        //Console.WriteLine("Enter the path of directory to save the attachment:");
        //inputValue = Console.ReadLine();
        //File.WriteAllBytes(inputValue! + @"\test.rar", result);
        //Environment.CurrentDirectory = inputValue!;
        //var enrchivator = Process.Start(@"C:\Program Files\7-Zip\7z.exe", new[] { "x", "test.rar", "-o\".\"" });
        //while (!enrchivator.HasExited)
        //{
        //    Thread.Sleep(100);
        //}

        //if (enrchivator.ExitCode != 0)
        //{
        //    Console.WriteLine(enrchivator.StandardOutput.ReadToEnd());
        //    Console.WriteLine("Error!!! ExitCode=" + enrchivator.ExitCode);
        //}

    }

}






