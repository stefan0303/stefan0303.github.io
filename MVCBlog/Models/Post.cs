using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCBlog.Models
{
    public class Post
    {
        public Post()
        {
            this.Date = DateTime.Now;
            this.Comments=new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Body { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        
        //public Nullable<System.DateTime> EstPurchaseDate { get; set; }

        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public ApplicationUser Author { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
