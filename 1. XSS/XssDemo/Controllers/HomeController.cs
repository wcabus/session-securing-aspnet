using System.Collections.Generic;
using System.Web.Mvc;
using XssDemo.Models;

namespace XssDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly static List<IBlogComment> _comments = new List<IBlogComment>(); 

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























        public ActionResult AddComment2()
        {
            return View("AddComment");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddComment2(NewBlogComment2 model)
        {
            if (!ModelState.IsValid)
            {
                return View("AddComment", model);
            }

            _comments.Add(model);

            return RedirectToAction("Index");
        }

        public ActionResult AddComment3()
        {
            return View("AddComment");
        }

        [HttpPost]
        public ActionResult AddComment3(NewBlogComment2 model)
        {
            if (!ModelState.IsValid)
            {
                return View("AddComment", model);
            }

            _comments.Add(model);

            return RedirectToAction("Index");
        }
    }
}