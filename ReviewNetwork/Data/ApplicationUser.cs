using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewNetwork.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();    
    }
}
