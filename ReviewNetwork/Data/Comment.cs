using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewNetwork.Data
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Body { get; set; }
        public Review Review { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
