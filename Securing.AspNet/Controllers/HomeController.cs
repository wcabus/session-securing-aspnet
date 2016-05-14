using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Securing.AspNet.Models;

namespace Securing.AspNet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjaxForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjaxPostAction(SimpleDataModel model)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ModelState.ToJson());
            }

            model.Name = $"Hello {model.Name}!";
            return Json(model);
        }
    }
}