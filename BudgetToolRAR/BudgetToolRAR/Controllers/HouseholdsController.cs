using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BudgetToolRAR.Models;
using Microsoft.AspNet.Identity;

namespace BudgetToolRAR.Controllers
{
    public class HouseholdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Households
        public ActionResult Index()
        {
            return View(db.Households.ToList());
        }

        // GET: Households/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // GET: Households/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Households.Add(household);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(household);
        }

        // GET: Households/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Entry(household).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(household);
        }

        public ActionResult HouseList()
        {
            //var UserId = User.Identity.GetUserId();
            var user = db.Users.Find(User.Identity.GetUserId());
            Household hh = db.Households.Find(user.HouseholdId);
            //var HouseNumber = db.Users.Where(u => );
            return View(hh.Users.ToList());
        }


        // GET: Households/InviteUser
        public ActionResult InviteUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InviteUser( Invite invite)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            invite.HouseholdId = user.HouseholdId.Value;
            invite.Secret = Utilities.GenerateSecretCode();
            db.Invites.Add(invite);
            db.SaveChanges();
            new EmailService().SendAsync(new IdentityMessage
            {
                Subject = "Join Household",
                Destination = invite.Email,
                Body = "You have been invited to join a household in the budget tool. Select -Join- on log in, and input the code: " + invite.Secret +".\n" +
                        "Or, <a href=\"" + Url.Action("RegisterLb", "Account", new { secret = invite.Secret }, "https") +"\">Click here</a> to join."
            });

            return RedirectToAction("HouseList", "Households");
        }

        // GET: Households/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Household household = db.Households.Find(id);
            db.Households.Remove(household);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Projects/Delete/5
        public ActionResult RemoveUser()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
                                                // User.Identity.GetUserId gets the information from the cookie
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Projects/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveUserConfirmed()
        {
            var user = db.Users.Find(
                User.Identity.GetUserId());
            user.HouseholdId = null;
            db.SaveChanges();
            return RedirectToAction("Landing","Home");
        }

        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(ApplicationUser userInfo, string email, string displayname, int HouseId)
        {
            if (ModelState.IsValid)
            {
                userInfo.Email = email;
                userInfo.DisplayName = displayname;

                db.Users.Add(userInfo);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = HouseId });
            }
            return View();
        }

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
