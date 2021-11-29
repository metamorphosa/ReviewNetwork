using System.Collections.Generic;

namespace ReviewNetwork.Data
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int CategoryId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public int LikeCount { get; set; } = 0;
    }
}
