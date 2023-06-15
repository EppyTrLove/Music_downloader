namespace Music_downloader
{
    public class PostModel : IEqualityComparer<PostModel>
    {
        public PostModel(long id, string name, int likes, int reposts, DateTime date)
        {
            Id = id;
            Name = name;
            Likes = likes;
            Reposts = reposts;
            Date = date;
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public int Likes { get; set; }
        public int Reposts { get; set; }
        public DateTime Date { get; set; }
        public AttachmentModel[] Attachments { get; set; }
        public PersonModel Person { get; set; }

        public bool Equals(PostModel? x, PostModel? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id;
        }

        public int GetHashCode(PostModel obj)
        {
            return obj.Id.GetHashCode();
        }

        public class AttachmentModel
        {
            public string Link { get; set; }
        }
        public class PersonModel
        {
            public long id { get; set; }
        }
    }
}