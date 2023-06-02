
namespace Music_downloader
{
    public class PostModel
    {
        public PostModel(string link, string name, int likes, int reposts)
        {
            Link = link;
            Name = name;
            Likes = likes;
            Reposts = reposts;
        }
        public string Link { get; set; }
        public string Name { get; set; }
        public int Likes { get; set; }
        public int Reposts { get; set; }
    }
}
