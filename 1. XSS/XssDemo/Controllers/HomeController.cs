using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XssDemo.Models;

namespace XssDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly static List<NewBlogComment> _comments = new List<NewBlogComment>(); 

        // GET: Home
        public ActionResult Index()
        {
            return View(_comments);
        }

        public ActionResult AddComment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddComment(NewBlogComment model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _comments.Add(model);

            return RedirectToAction("Index");
        }
    }
}