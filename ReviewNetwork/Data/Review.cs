using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewNetwork.Data
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Category Category { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public int Rating { get; set; } 
        public int Likes { get; set; } = 0;
    }
}
