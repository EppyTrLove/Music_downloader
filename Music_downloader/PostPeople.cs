
namespace Music_downloader
{
    public class PostPeople : IEqualityComparer<PostPeople>
    {
        public PostPeople(long? postId, long[] likerIds)
        {
            PostId = postId;
            LikerIds = likerIds;
        }
        public bool Equals(PostPeople? x, PostPeople? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.PostId == y.PostId;
        }

        public int GetHashCode(PostPeople obj)
        {
            return obj.PostId.GetHashCode();
        }

        public long? PostId { get; set; }
        public long[] LikerIds { get; set; }
    }
}
