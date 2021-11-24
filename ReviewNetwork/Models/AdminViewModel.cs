using ReviewNetwork.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewNetwork.Models
{
    public class AdminViewModel
    {
        public ICollection<ApplicationUser> Users { get; set; }

    }
}
