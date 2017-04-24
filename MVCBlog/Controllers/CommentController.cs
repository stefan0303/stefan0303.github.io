using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBlog.Models;
using Microsoft.AspNet.Identity;

namespace MVCBlog.Controllers
{
    public class CommentController : Controller
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        // GET: Comment
        //public ActionResult PostDetails(string submitComment,string post)
        //{
        //  //  return View(db.Comments.Where(c=>c.Post.AuthorId==post));
        //}




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
        public ActionResult Create([Bind(Include = "Id,Title,Body,Date")] Comment comment)
        {

            if (ModelState.IsValid)
            {
                comment.UserId = User.Identity.GetUserId();
                //comment.PostId = 
                db.Comments.Add(comment);
                db.SaveChanges();

                //TODO: redirect to post
                return RedirectToAction("Index");
            }

            return View(comment);
        }
    }
}