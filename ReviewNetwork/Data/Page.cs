using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewNetwork.Data
{
    public class Page
    {
        public int PageId { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
