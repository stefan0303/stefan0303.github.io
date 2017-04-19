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
 
        public ActionResult IndexTwo(string search)
        {

            var posts = db.Posts.Where(x => x.Body.Contains(search)||x.Title.Contains(search)).ToList();
         
                return View(posts);
            //if (posts.Count == 0)
            //{
            //    return View(Content("There are no results with key word {0}", search));
            //}

        }
    }
}