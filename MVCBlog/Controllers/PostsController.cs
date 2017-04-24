using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MVCBlog.Models;
using MVCBlog.ViewModels;

namespace MVCBlog.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // All  Posts
        
        public ActionResult Index()
        {
            return View(db.Posts.Include(p => p.Author).ToList());
        }
        public ActionResult GetSearch()
        {

            var posts = db.Posts.Include(p => p.Author).OrderByDescending(p => p.Date).Take(1);
            return View(posts.ToList());

            // db.Posts.Where(p => p.Body.Contains(Search().ToString())).ToList()

        }

        // GET: Posts/Details/5
        [Authorize]
        //public ActionResult Details(int? id)
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                
                return HttpNotFound();
            }
            var viewModel = new PostCommentViewModel(id);
            ModelState.Clear();
            return View(viewModel);

        }
        [Authorize]
        public ActionResult ShowDateTime()
        {
            DateTime date=DateTime.Now;
            string dateTime = date.ToString();
            return View(dateTime);
        }

        // GET: Posts/Create
        [Authorize]
        public ActionResult Create()
        {            
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Title,Body,Date")] Post post)
        {
            
            if (ModelState.IsValid)
            {
                post.AuthorId = User.Identity.GetUserId();
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,Date,AuthorId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [Authorize(Roles = "Administrators")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // TODO: resolve ambiguous signature instead of Request.Params
        public ActionResult AddComment(int id = 0)
        {
            if ("true".Equals(Request.Params["add_comment"]))
            {
                
                Comment comment = new Comment();

                comment.PostId = Convert.ToInt32(Request.Params["post_id"]);
                comment.Post = db.Posts.FirstOrDefault(p => p.Id == comment.PostId);

                comment.UserId = User.Identity.GetUserId();
                comment.User = db.Users.FirstOrDefault(u => u.Id == comment.UserId);

                comment.Body = Request.Params["comment_content"];
                comment.DateTime = DateTime.Now;

                db.Comments.Add(comment);
                db.SaveChanges();
              
                return View();
            }
            return View();
        }
        
        //public ActionResult AddComment(Comment comment, int id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // NEW
        //       // comment.Moderated = false;

        //        comment.PostId = id;
        //        db.Comments.Add(comment);
        //        db.SaveChanges();
        //        return RedirectToAction("Details", "Blog", new { id = id });
        //    }
        //    return RedirectToAction("Details", "Blog", new { id = id });
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            
            base.Dispose(disposing);
        }
    }
}
