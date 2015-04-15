using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Personal_Website_2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your About Me page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.MessageSent = TempData["MessageSent"];
            return View();
        }
        public ActionResult Resume()
        {
            ViewBag.Message = "Your resume page.";

            return View();
        }

        public ActionResult Other()
        {
            ViewBag.Message = "Your other page.";

            return View();
        }

        public ActionResult Portfolio()
        {
            ViewBag.Message = "Your portfolio page.";

            return View();
        }
             public ActionResult JS_Exercises()
        {
            ViewBag.Message = "Your JS Exercises page.";

            return View();
        }

             public ActionResult Blog()
             {
                 ViewBag.Message = "Your Blog page.";

                 return View();
             }
        
    }
}