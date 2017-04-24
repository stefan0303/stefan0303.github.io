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
        public Comment()
        {
            this.DateTime=DateTime.Now;
        }
        [Key]
        public int Id { get; set; }

        public int PostId { get; set; }
        [Required]
        public virtual Post Post { get; set; }

        public DateTime DateTime { get; set; }

        public string UserId { get; set; } //can be anonymous user 
        public virtual ApplicationUser User { get; set; }//the user make the comment

        [Required]
        [StringLength(200)]
        public string Body { get; set; }
    }

}