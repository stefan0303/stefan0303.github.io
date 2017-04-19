using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using MVCBlog.Models;

namespace MVCBlog.Controllers
{   
   
    public class SearchController : Controller
    {
        // GET: Search
        private static ApplicationDbContext db = new ApplicationDbContext();
 
        public ActionResult IndexTwo(string searchBy, string search)
        {
 
            switch (searchBy)
            {
                case "AllText":
                    var posts = db.Posts.Where(x => x.Body.Contains(search) || x.Title.Contains(search)).ToList();
                    return View(posts);
                case "Title":
                     posts = db.Posts.Where(x=>x.Title.Contains(search)).ToList();
                    return View(posts);
                case "Post":
                     posts = db.Posts.Where(x => x.Body.Contains(search)).ToList();
                    return View(posts);
            }
           return View(db.Posts.Where(x => x.Body.Contains(search) || x.Title.Contains(search)).ToList()); 
        }
    }
}