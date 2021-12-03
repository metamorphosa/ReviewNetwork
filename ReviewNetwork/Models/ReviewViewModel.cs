using Microsoft.AspNetCore.Http;
using ReviewNetwork.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewNetwork.Models
{
    public class ReviewViewModel
    {
        public string Name { get; set; }
        public Review Review { get; set; }
        public IList<string> Tags { get; set; }
    }
}
