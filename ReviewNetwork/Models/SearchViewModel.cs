using ReviewNetwork.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewNetwork.Models
{
    public class SearchViewModel
    {
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
