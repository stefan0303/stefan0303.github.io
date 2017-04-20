using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MVCBlog.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public int PostId { get; set; }

        public  Post Post { get; set; }
        [Required]
        [StringLength(200)]
        public string Body { get; set; }
    }
}