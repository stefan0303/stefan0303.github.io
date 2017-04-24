using MVCBlog.Models;
using System.Collections.Generic;
using System.Linq;



namespace MVCBlog.ViewModels
{
    public class PostCommentViewModel
    {
        public PostCommentViewModel()
        {
            this.Comments=new HashSet<Comment>();
        }
        public Post Post { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
       // public List<Comment> Comments { get; set; }

        private ApplicationDbContext db = new ApplicationDbContext();

        public PostCommentViewModel(int postId)
        {

            Post = db.Posts.First(x => x.Id == postId);
            Comments = db.Comments.Where(c => c.PostId == postId).ToList();
            db.SaveChanges();

        }
        
    }
}