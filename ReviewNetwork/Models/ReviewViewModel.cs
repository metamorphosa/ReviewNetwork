using Microsoft.AspNetCore.Http;
using ReviewNetwork.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewNetwork.Models
{
    public class ReviewViewModel
    {
        [Required, StringLength(100)]
        public string Title { get; set; }
        [Required, StringLength(5000)]
        public string Body { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public ICollection<Tag> Tags { get; set; }
    }
}
