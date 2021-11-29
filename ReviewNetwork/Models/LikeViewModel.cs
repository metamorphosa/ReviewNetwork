using ReviewNetwork.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewNetwork.Models
{
    public class LikeViewModel
    {
        public ICollection<Review> Reviews { get; set; }
        public Review Review { get; set; }
        public int ReviewId { get; set; }
        public Like Like { get; set; } = new();
        public ApplicationUser CurrentUser { get; set; }
    }
}
