using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewNetwork.Data
{
    public class Like
    {
        public int LikeId { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ReviewId { get; set; }
        public Review Review { get; set; }
        public bool IsLiked { get; set; }
    }
}
