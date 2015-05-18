using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BudgetToolRAR.Models;

namespace BudgetToolRAR.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Landing()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult HouseList()
        {
            //var UserId = User.Identity.GetUserId();
            var user = db.Users.Find(User.Identity.GetUserId());
            Household hh = db.Households.Find(user.HouseholdId);
            //var HouseNumber = db.Users.Where(u => );
            return View(hh.Users.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}